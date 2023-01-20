using GeekShoppingClient.Web.Service;
using GeekShoppingClient.Web.Service.IServices;

namespace GeekShoppingClient.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegistrarServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
        }
    }
}
