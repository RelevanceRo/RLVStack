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
using Backend.Features.Roles._Shared;
using Backend.Features.Users._Shared;

namespace Backend.Features.Roles;

public sealed class GetRoles
{
    internal sealed class Handler(KrafterContext db) : IScopedHandler
    {
        public async Task<Response<PaginationResponse<RoleDto>>> GetListAsync(
            [AsParameters]GetRequestInput requestInput,
            CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.New<KrafterRole>(true);

            if (!string.IsNullOrWhiteSpace(requestInput.Id))
            {
                predicate = predicate.And(c => c.Id == requestInput.Id);
            }

            var query = db.Roles
                .Where(predicate)
                .Select(x => new RoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedById = x.CreatedById,
                    IsDeleted = x.IsDeleted,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy != null ? new UserInfo
                    {
                        Id = x.CreatedBy.Id,
                        FirstName = x.CreatedBy.FirstName,
                        LastName = x.CreatedBy.LastName,
                        CreatedOn = x.CreatedBy.CreatedOn
                    } : null,
                    UpdatedBy = x.UpdatedBy != null ? new UserInfo
                    {
                        Id = x.UpdatedBy.Id,
                        FirstName = x.UpdatedBy.FirstName,
                        LastName = x.UpdatedBy.LastName,
                        CreatedOn = x.UpdatedBy.CreatedOn
                    } : null,
                    UpdatedById = x.UpdatedById,
                    UpdatedOn = x.UpdatedOn,
                    DeleteReason = x.DeleteReason
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
                        ((c.Name ?? "").ToLower().Contains(filter)));
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

            var result = new PaginationResponse<RoleDto>(
                items,
                totalCount,
                requestInput.SkipCount,
                requestInput.MaxResultCount);

            return new Response<PaginationResponse<RoleDto>>
            {
                Data = result
            };
        }
    }

    public sealed class Route : IRouteRegistrar
    {
        public void MapRoute(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var roleGroup = endpointRouteBuilder.MapGroup(KrafterRoute.Roles)
                .AddFluentValidationFilter();

            roleGroup.MapGet("/get", async (
                [FromServices] Handler handler,
                [AsParameters] GetRequestInput requestInput,
                CancellationToken cancellationToken) =>
            {
                var res = await handler.GetListAsync(requestInput, cancellationToken);
                return Results.Json(res, statusCode: res.StatusCode);
            })
            .Produces<Common.Models.Response<PaginationResponse<RoleDto>>>()
            .MustHavePermission(KrafterAction.View, KrafterResource.Roles);
        }
    }
}
