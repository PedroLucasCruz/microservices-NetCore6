﻿using GeekShoppingApp.Identity.Inicializer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GeekShoppingApp.Identity.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegistrarServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddScoped<IDbInitializer, DbInitializer>();

        }
    }
}
