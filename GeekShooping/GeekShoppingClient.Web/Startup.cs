
using GeekShoppingClient.Web.Services.IServices;
using GeekShoppingClient.Web.Services;

using GeekShoppingClient.Web.Configurations;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace GeekShoppingClient.Web
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
          
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddControllersWithViews();

            #region Configuração do service Cliente
            
            services.RegistrarServices(Configuration);

            //services.AddHttpClient<IProductService, ProductService>(
            //    c => c.BaseAddress = new Uri(Configuration["ServiceUrls:ProductAPISwagger"]));

            //services.AddHttpClient<IAutenticacaoService, AutenticacaoService>(
            //  c => c.BaseAddress = new Uri(Configuration["ServiceUrls:AutenticacaoAPI"]));
            #endregion

        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            app.UseApiConfiguration(environment);
       
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


            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            startup.Configure(app, app.Environment);
                    

            app.Run();

            return WebAppBuilder;

        }
    }
}
