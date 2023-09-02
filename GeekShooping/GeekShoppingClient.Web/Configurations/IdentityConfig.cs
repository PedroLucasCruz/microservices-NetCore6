using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GeekShoppingClient.Web.Configurations
{
    public static class IdentityConfig
    {
  

        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
          

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer("JwtBearer", bearerOptions => //quando você usa AddJwtBearer, você está dizendo que está adicionado suporte para este tipo especifico de token
            {
           
                bearerOptions.Authority = "https://localhost:5001/"; //endereco do servico do identity em geekShoppingApp.Identity              
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                    
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    //ValidateLifetime = true,
                    //ValidateIssuerSigningKey = true,
                    //ValidIssuer = "MeuSistema",
                    //ValidAudience = "https://localhost",
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
                };
            })


            //Configuração do Cookie 
              .AddCookie(JwtBearerDefaults.AuthenticationScheme, options =>
              {
                  options.LoginPath = "/Url"; //quando o usuario não estiver logado e quiser encaminhar para uma area da aplicação
                  options.AccessDeniedPath = "/UrlAcessoNegado"; //Caso o Usuario navegue para uma area da aplicação que ele não tenha acesso
                  options.Cookie = new CookieBuilder
                  {
                      Name = "FedAuth",
                      HttpOnly = true,
                      IsEssential = true,
                      SameSite = SameSiteMode.None,
                      SecurePolicy = CookieSecurePolicy.Always,
                  };

                  options.ExpireTimeSpan = new TimeSpan(0, 0, int.Parse("1000000"), 0);
                  options.SlidingExpiration = false;

                  // TODO: Verificar se seria o caso de direcionar para uma ação de controller
                  options.Events.OnRedirectToAccessDenied = context =>
                  {
                      context.Response.StatusCode = 403;
                      context.Response.WriteAsync("Acesso Negado!");
                      return Task.CompletedTask;
                  };
              });


                //Configuração de escopo de claims identity
                //services.AddAuthorization(options =>
                //        options.AddPolicy("role", policy =>
                //        {
                //            policy.RequireAuthenticatedUser();
                //            policy.RequireClaim("role", "AindaNaoTemEscopoDeClaims");
                //        }
                //));
        }

        public static void UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
