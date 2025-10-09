using Backend.Api;
using Backend.Api.Authorization;
using Backend.Common;
using Backend.Common.Auth.Permissions;
using Backend.Common.Models;
using Backend.Features.Users._Shared;
using Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Users;

public sealed class DeleteUser
{
    internal sealed class Handler(
        UserManager<KrafterUser> userManager,
        KrafterContext db) : IScopedHandler
    {
        public async Task<Response> DeleteAsync(DeleteRequestInput requestInput)
        {
            var user = await userManager.FindByIdAsync(requestInput.Id);
            if (user is null)
            {
                return new Response()
                {
                    IsError = true,
                    Message = "User Not Found",
                    StatusCode = 404
                };
              
            }

            if (user.IsOwner)
            {
                return new Response()
                {
                    IsError = true,
                    Message = "Owner cannot be deleted",
                    StatusCode = 403
                };
               
            }

            user.IsDeleted = true;
            user.DeleteReason = requestInput.DeleteReason;
            db.Users.Update(user);

            var userRoles = await db.UserRoles
                .Where(c => c.UserId == requestInput.Id)
                .ToListAsync();

            foreach (var userRole in userRoles)
            {
                userRole.IsDeleted = true;
            }

            await db.SaveChangesAsync([nameof(KrafterUser)]);

            return new Response();
        }
    }

    public sealed class Route : IRouteRegistrar
    {
        public void MapRoute(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var userGroup = endpointRouteBuilder.MapGroup(KrafterRoute.Users)
                .AddFluentValidationFilter();

            userGroup.MapPost("/delete", async (
                [FromBody] DeleteRequestInput request,
                [FromServices] Handler handler) =>
            {
                var res = await handler.DeleteAsync(request);
                return Results.Json(res, statusCode: res.StatusCode);
            })
            .Produces<Response>()
            .MustHavePermission(KrafterAction.Delete, KrafterResource.Users);
        }
    }
}
