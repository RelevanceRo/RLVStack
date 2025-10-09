﻿using Backend.Api;
using Backend.Common;
using Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Backend.Common.Models;

namespace Backend.Features.AppInfo
{
 public  sealed  class Get
    {

        public static class BuildInfo
        {
            public static string DateTimeUtc { get; set; } = "#DateTimeUtc";
            public static string Build { get; set; } = "#Build";

        }

        public sealed class Route : IRouteRegistrar
        {
            public void MapRoute(IEndpointRouteBuilder endpointRouteBuilder)
            {
                var routeGroupBuilder = endpointRouteBuilder.MapGroup(KrafterRoute.AppInfo);
                routeGroupBuilder.MapGet("/", ([FromServices] Handler handler, CancellationToken cancellationToken) =>
                {
                    var res = handler.GetAppInfo();
                    return res;
                });
            }
        }

        public class Handler : IScopedHandler
        {

            public async Task<Response<string>> GetAppInfo()
            {
                var res = new Response<string>()
                {
                    Data = $"Backend version {BuildInfo.Build}, built on {BuildInfo.DateTimeUtc}, running on {RuntimeInformation.FrameworkDescription}"

                };
                return res;
            }
        }
    }
}
