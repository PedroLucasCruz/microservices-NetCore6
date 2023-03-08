using GeekShoppingClient.Web.Configurations.IConfig;
using GeekShoppingClient.Web.Extensions;
using GeekShoppingClient.Web.Services;

using GeekShoppingClient.Web.Services.IServices;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GeekShoppingClient.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegistrarServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IProductService, ProductService>(
                c => c.BaseAddress = new Uri(configuration["ServiceUrls:ProductAPISwagger"]));

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>(
                c => c.BaseAddress = new Uri(configuration["ServiceUrls:AutenticacaoAPI"]));

            services.AddScoped<IJwtConfig, JwtConfig>();

            //Cada request é uma representação unica, microsoft recomenda que seja AddSingleton
           // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, CoreUser>();
        }
    }
}
