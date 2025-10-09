﻿using Backend.Api;
using Backend.Api.Authorization;
using Backend.Application.Common;
using Backend.Common;
using Backend.Common.Auth.Permissions;
using Backend.Common.Models;
using Backend.Features.Users._Shared;
using Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Tenants
{
    public sealed  class Delete
    {

        internal sealed class Handler(TenantDbContext dbContext,KrafterContext krafterContext): IScopedHandler
        {
            public async Task<Response> DeleteAsync(DeleteRequestInput requestInput)
            {
                var tenant = await dbContext.Tenants.AsNoTracking().FirstOrDefaultAsync(c => c.Id == requestInput.Id);
                if (tenant is null)
                {
                    throw new KrafterException(
                        "Unable to find tenant, please try again later or contact support.");
                }
                if (tenant.Id == KrafterInitialConstants.RootTenant.Id)
                {
                    throw new ForbiddenException(
                        "You cannot delete the root tenant.");
                }

                tenant.IsDeleted = true;
                tenant.DeleteReason = requestInput.DeleteReason;
                dbContext.Tenants.Update(tenant);
                await dbContext.SaveChangesAsync();
                await krafterContext.SaveChangesAsync([nameof(Tenant)]);
                return new Response();
            }
        }

        public sealed class Route : IRouteRegistrar
        {
            public void MapRoute(IEndpointRouteBuilder endpointRouteBuilder)
            {
                var tenant = endpointRouteBuilder.MapGroup(KrafterRoute.Tenants).AddFluentValidationFilter();

                tenant.MapPost("/delete", async
                ([FromBody] DeleteRequestInput requestInput,
                    [FromServices] Handler handler) =>
                {
                    var res = await handler.DeleteAsync(requestInput);
                    return Results.Json(res, statusCode: res.StatusCode);

                })

                .Produces<Common.Models.Response>()
                    .MustHavePermission(KrafterAction.Delete, KrafterResource.Tenants);


            }
        }

    }
} 
