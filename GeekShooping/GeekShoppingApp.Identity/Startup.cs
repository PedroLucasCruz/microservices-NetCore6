#region using necessário dos namespaces da aplicação
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
#endregion

using GeekShoppingApp.Identity.Data;
using GeekShoppingApp.Identity.Extensions;

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

            #region //JWT

            var appSettingsSection = Configuration.GetSection("AppSettings"); //Pegou o nó do arquivo de configuração appSettings
            services.Configure<AppSettings>(appSettingsSection); //Configure o appsetting atraves do appseting section, configurando no middlaware/pipelane
                                                                 //para que a classe AppSettings represente os dados da sessão appSettingsSection vindo das configurações
            
            var appSettings = appSettingsSection.Get<AppSettings>(); //Obtem a AppSettings já populada
            var key = Encoding.ASCII.GetBytes(appSettings.Secret); //Transforma a chave do token em uma sequencia de bytes para uso mais adiante

            //O padrão de autenticação depende da forma de implementação
            //neste caso o privider de autenticação que está sendo utilizado é AuthenticationScheme do Json web Token
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => //quando você usa AddJwtBearer, você está dizendo que está adicionado suporte para este tipo especifico de token

            {
                bearerOptions.RequireHttpsMetadata = true; //requerer acesso pelo https por segurança 
                bearerOptions.SaveToken = true; //dizer que o token vai ser guardado na estanci assim que o login for realizado com sucesso
                bearerOptions.TokenValidationParameters = new TokenValidationParameters //parametro de validação do token logo a baixo
                {
                    ValidateIssuerSigningKey = true, //Audiencia quer dizer onde o token pode ser utilizado, pode ser utilziado em dominio X ou Y, você vai validar com base na assinatura
                    IssuerSigningKey = new SymmetricSecurityKey(key), //Assinatura do emissor criada a partida da classe SymmetricSecurityKey
                    ValidateIssuer = true, //Issuer valido para que o token seja valida apenas para dentro das apis que você quiser, então sempre validar o emissor, não aceitar o token de outros emissores para as apis
                    ValidateAudience = true, //Validar para quais dominios esse token é válido
                   
                    // ValidAudiences = appSettings.Emissor, //valida varios pontos de audiencia 
                    
                    ValidAudience = appSettings.ValidoEm, //Criar a audiencia de para onde o token será valido
                    ValidIssuer = appSettings.Emissor //Issuer valido 

                    //Caso não seja o mesmo token dentro da mesma audiencia o token não vai ser válido para api

                };
            });

            #endregion

            // services.AddRazorPages();
            services.AddControllers();

            services.AddSwaggerGen( c => {

                c.SwaggerDoc("v1", new OpenApiInfo
                { 
                 Title = "GeekShopping", //Titulo da sua api
                 Description = "Esta API faz parte do GeekShopping ", //Descrição
                 Contact = new OpenApiContact() { Name = "Pedro Cruz", Email = "pedrolucas11@live.com" },
                 License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") } //licensa da api, pode ser colocado qualquer link
                
                });
            });

        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");            
            });


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
