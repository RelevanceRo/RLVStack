using Backend.Api;
using Backend.Api.Authorization;
using Backend.Common;
using Backend.Common.Auth;
using Backend.Common.Auth.Permissions;
using Backend.Common.Models;
using Backend.Features.Auth;
using Backend.Features.Roles._Shared;
using Backend.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Roles;

public sealed class UpdateRolePermissions
{
    public sealed class UpdateRolePermissionsRequest
    {
        public string RoleId { get; set; } = default!;
        public List<string> Permissions { get; set; } = [];
    }

    internal sealed class Handler(
        RoleManager<KrafterRole> roleManager,
        KrafterContext db) : IScopedHandler
    {
        public async Task<Response> UpdatePermissionsAsync(
            UpdateRolePermissionsRequest request,
            CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByIdAsync(request.RoleId);

            if (role is null)
            {
                return new Response
                {
                    IsError = true,
                    StatusCode = 404,
                    Message = "Role Not Found"
                };
            }

            if (role.Name == KrafterRoleConstant.Admin)
            {
                return new Response
                {
                    IsError = true,
                    StatusCode = 403,
                    Message = "Not allowed to modify Permissions for this Role."
                };
            }

            var currentClaims = await roleManager.GetClaimsAsync(role);

            // Remove permissions that were previously selected
            foreach (var claim in currentClaims.Where(c => request.Permissions.All(p => p != c.Value)))
            {
                var removeResult = await roleManager.RemoveClaimAsync(role, claim);
                if (!removeResult.Succeeded)
                {
                    return new Response
                    {
                        IsError = true,
                        StatusCode = 400,
                        Message = $"Update permissions failed: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}"
                    };
                }
            }

            // Add all permissions that were not previously selected
            foreach (string permission in request.Permissions.Where(c => currentClaims.All(p => p.Value != c)))
            {
                if (!string.IsNullOrEmpty(permission))
                {
                    db.RoleClaims.Add(new KrafterRoleClaim
                    {
                        RoleId = role.Id,
                        ClaimType = KrafterClaims.Permission,
                        ClaimValue = permission,
                    });
                    await db.SaveChangesAsync(cancellationToken);
                }
            }

            return new Response
            {
                Message = "Role permissions updated successfully"
            };
        }
    }

    internal sealed class Validator : AbstractValidator<UpdateRolePermissionsRequest>
    {
        public Validator()
        {
            RuleFor(p => p.RoleId)
                .NotEmpty().WithMessage("Role ID is required");

            RuleFor(p => p.Permissions)
                .NotNull().WithMessage("Permissions list cannot be null");
        }
    }

    public sealed class Route : IRouteRegistrar
    {
        public void MapRoute(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var roleGroup = endpointRouteBuilder.MapGroup(KrafterRoute.Roles)
                .AddFluentValidationFilter();

            roleGroup.MapPut("/update-permissions", async (
                [FromBody] UpdateRolePermissionsRequest request,
                [FromServices] Handler handler,
                CancellationToken cancellationToken) =>
            {
                var res = await handler.UpdatePermissionsAsync(request, cancellationToken);
                return Results.Json(res, statusCode: res.StatusCode);
            })
            .Produces<Response>()
            .MustHavePermission(KrafterAction.Update, KrafterResource.Roles);
        }
    }
}
