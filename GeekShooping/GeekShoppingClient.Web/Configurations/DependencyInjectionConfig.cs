using GeekShoppingClient.Web.Services;
using GeekShoppingClient.Web.Services.IServices;

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
        }
    }
}
