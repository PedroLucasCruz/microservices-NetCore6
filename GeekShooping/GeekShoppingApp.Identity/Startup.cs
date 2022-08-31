using GeekShoppingApp.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace GeekShoppingApp.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
              
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
            options =>
            options.UseSqlServer(Configuration.GetConnectionString("GeekShoppingIdentityContextConnection")));

            //Configuração de suporte ao Identity             
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>() //Adicionando as regras de perfil de usuario
                .AddEntityFrameworkStores<ApplicationDbContext>() //especificando que você vai trabalhar com eneity framework
                .AddDefaultTokenProviders(); //Token que são gerados, criptografia para reconhecimento e autenticação 

            // services.AddRazorPages();
            services.AddControllers();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication(); //adicionado para autenticar a aplicação

            //Utilize os endpoint, as controller, rotas das controllers 
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllers(); //vai criar um mapeamento das controllers 
            });
        }
    }
}
