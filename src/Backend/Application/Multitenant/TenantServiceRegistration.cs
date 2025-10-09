﻿using Backend.Common.Interfaces;

namespace Backend.Application.Multitenant
{
    public static class TenantServiceRegistration
    {
        public static IServiceCollection AddTenantServices(this IServiceCollection services)
        {
            services.AddScopedAs<CurrentTenantService>(new[] {
                typeof(ITenantGetterService),
                typeof(ITenantSetterService)
            });

            return services;
        }

        private static IServiceCollection AddScopedAs<T>(this IServiceCollection services, IEnumerable<Type> types)
            where T : class
        {
            // register the type first
            services.AddScoped<T>();
            foreach (var type in types)
            {
                // register a scoped 
                services.AddScoped(type, svc =>
                {
                    var rs = svc.GetRequiredService<T>();
                    return rs;
                });
            }
            return services;
        }
    }
}
