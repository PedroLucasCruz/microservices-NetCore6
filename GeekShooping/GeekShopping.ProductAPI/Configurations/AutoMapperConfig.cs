using GeekShopping.ProductAPI.AutoMapper;

namespace GeekShopping.ProductAPI.Configurations
{
    //Segunda forma de configurar o auto mapper
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(MappingProfile), typeof(MappingProfile));
        }
    }
}
