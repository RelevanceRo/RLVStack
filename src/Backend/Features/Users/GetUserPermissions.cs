using Backend.Api;
using Backend.Common;
using Backend.Common.Auth;
using Backend.Common.Auth.Permissions;
using Backend.Common.Interfaces.Auth;
using Backend.Common.Models;
using Backend.Features.Roles._Shared;
using Backend.Features.Users._Shared;
using Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Users;

public sealed class GetUserPermissions
{
    internal sealed class Handler(
        UserManager<KrafterUser> userManager,
        RoleManager<KrafterRole> roleManager,
        KrafterContext db) : IScopedHandler
    {
        public async Task<Response<List<string>>> GetPermissionsAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await db.Users.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == userId, cancellationToken);

            if (user is null)
            {
                return new Response<List<string>>
                {
                    IsError = true,
                    Message = "User Not Found",
                    StatusCode = 404
                };
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var permissions = new List<string>();

            foreach (var role in await roleManager.Roles.AsNoTracking()
                         .Where(r => userRoles.Contains(r.Name!) && r.IsDeleted == false)
                         .ToListAsync(cancellationToken))
            {
                permissions.AddRange(await db.RoleClaims.AsNoTracking()
                    .Where(rc =>
                        rc.RoleId == role.Id &&
                        rc.ClaimType == KrafterClaims.Permission &&
                        rc.IsDeleted == false)
                    .Select(rc => rc.ClaimValue!)
                    .ToListAsync(cancellationToken));
            }

            return  new Response<List<string>>()

            {
                Data = permissions.Distinct().ToList()
            };
        }
    }

    public sealed class Route : IRouteRegistrar
    {
        public void MapRoute(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var userGroup = endpointRouteBuilder.MapGroup(KrafterRoute.Users)
                .AddFluentValidationFilter();

            userGroup.MapGet("/permissions", async (
                [FromServices] Handler handler,
                [FromServices] ICurrentUser currentUser) =>
            {
                var res = await handler.GetPermissionsAsync(currentUser.GetUserId(), CancellationToken.None);
                return Results.Json(res, statusCode: res.StatusCode);
            })
            .Produces<Response<List<string>>>()
            .RequireAuthorization();
        }
    }
}
