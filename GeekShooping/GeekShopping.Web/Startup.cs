//using GeekShopping.Web.Services;
//using GeekShopping.Web.Services.IServices;

//namespace GeekShopping.Web
//{
//    public class Startup : IStartup
//    {
//        public IConfiguration Configuration { get; }
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }



//        public void ConfigureServices(IServiceCollection services)
//        {

//            //services.AddScoped<IProductRepository, ProductRepository>();

//            #region httpClientFactory

//            services.AddHttpClient<IProductService, ProductService>(c => c.BaseAddress = new Uri(Configuration["ServiceUrls:ProductAPI"])); //injecao de product service para uso do httpClienteFactory
            
//            #endregion 

//            services.AddControllers();
//            services.AddEndpointsApiExplorer();
         
//        }

//        public void Configure(WebApplication app, IWebHostEnvironment environment)
//        {
           

//            app.UseHttpsRedirection();
//            app.UseAuthorization();
//            app.MapControllers();
//        }
//    }
//    public interface IStartup
//    {
//        IConfiguration Configuration { get; }
//        void Configure(WebApplication app, IWebHostEnvironment environment);
//        void ConfigureServices(IServiceCollection services);
//    }

//    public static class StartExtensions
//    {
//        public static WebApplicationBuilder UseStartup<Tstartup>(this WebApplicationBuilder WebAppBuilder) where Tstartup : IStartup
//        {
//            var startup = Activator.CreateInstance(typeof(Tstartup), WebAppBuilder.Configuration) as IStartup;
//            if (startup == null) throw new ArgumentException("Classe Startup.cs inválida!");

//            startup.ConfigureServices(WebAppBuilder.Services);
//            var app = WebAppBuilder.Build();

//#if (DEBUG)

//#endif

//            startup.Configure(app, app.Environment);

//            app.Run();

//            return WebAppBuilder;

//        }
//    }
//}
