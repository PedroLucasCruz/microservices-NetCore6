using AutoMapper;
using GeekShopping.ProductAPI.Configurations;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       

        public void ConfigureServices(IServiceCollection services)
        {
            #region Mapper
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion Mapper
            #region Segunda forma de Configurar o Mapper 
            //Menos verbosa que o primeiro método
            services.AddAutoMapperConfiguration();
            #endregion 

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddDatabaseConfiguration(Configuration);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment environment);
        void ConfigureServices(IServiceCollection services);
    }

    public static class StartExtensions
    {
        public static WebApplicationBuilder UseStartup<Tstartup>(this WebApplicationBuilder WebAppBuilder) where Tstartup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(Tstartup), WebAppBuilder.Configuration) as IStartup;
            if (startup == null) throw new ArgumentException("Classe Startup.cs inválida!");

            startup.ConfigureServices(WebAppBuilder.Services);
            var app = WebAppBuilder.Build();

#if (DEBUG)

#endif

            startup.Configure(app, app.Environment);

            app.Run();

            return WebAppBuilder;

        }
    }
}
