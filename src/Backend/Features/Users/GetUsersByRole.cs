using Backend.Api;
using Backend.Api.Authorization;
using Backend.Common;
using Backend.Common.Auth.Permissions;
using Backend.Common.Extensions;
using Backend.Common.Models;
using Backend.Infrastructure.Persistence;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Backend.Features.Users._Shared;

namespace Backend.Features.Users;

public sealed class GetUsersByRole
{

    internal sealed class Handler(KrafterContext db) : IScopedHandler
    {
        public async Task<Response<PaginationResponse<UserInfo>>> GetByRoleAsync(
            string roleId,
            [AsParameters] GetRequestInput requestInput,
            CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.New<KrafterUserRole>(true);
            predicate = predicate.And(c => c.RoleId == roleId);

            var query = db.UserRoles
                .Where(predicate)
                .Include(c => c.User)
                .Select(x => new UserInfo
                {
                    Id = x.User.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    CreatedOn = x.User.CreatedOn
                });

            // Apply filters
            if (!string.IsNullOrEmpty(requestInput.Filter))
            {
                if (requestInput.Filter.Contains("!=") ||
                    requestInput.Filter.Contains("==") ||
                    requestInput.Filter.Contains(".Contains(") ||
                    requestInput.Filter.Contains(".StartsWith(") ||
                    requestInput.Filter.Contains(".EndsWith(") ||
                    requestInput.Filter.Contains("np("))
                {
                    query = query.Where(requestInput.Filter);
                }
                else
                {
                    var filter = requestInput.Filter.ToLower();
                    query = query.Where(c =>
                        ((c.FirstName ?? "").ToLower().Contains(filter)) ||
                        ((c.LastName ?? "").ToLower().Contains(filter)));
                }
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(requestInput.OrderBy))
            {
                query = query.OrderBy(requestInput.OrderBy);
            }

            var items = await query
                .PageBy(requestInput)
                .ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            var result = new PaginationResponse<UserInfo>(
                items,
                totalCount,
                requestInput.SkipCount,
                requestInput.MaxResultCount);

            return new Response<PaginationResponse<UserInfo>>
            {
                Data = result,
                IsError = false,
                StatusCode = 200,
            }
            ;

        }
    }

    public sealed class Route : IRouteRegistrar
    {
        public void MapRoute(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var userGroup = endpointRouteBuilder.MapGroup(KrafterRoute.Users)
                .AddFluentValidationFilter();

            userGroup.MapGet("/by-role/{roleId}", async (
                [FromRoute] string roleId,
                [FromServices] Handler handler,
                [AsParameters] GetRequestInput requestInput,
                CancellationToken cancellationToken) =>
            {
                var res = await handler.GetByRoleAsync(roleId, requestInput, cancellationToken);
                return Results.Json(res, statusCode: res.StatusCode);
            })
            .Produces<Response<PaginationResponse<UserInfo>>>()
            .MustHavePermission(KrafterAction.View, KrafterResource.Users);
        }
    }
}
