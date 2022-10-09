#region using necessário dos namespaces da aplicação
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

using GeekShoppingApp.Identity.Configuration;

namespace GeekShoppingApp.Identity
{
    public class Startup
    {

        #region // trecho refatorado para configuracao a baixo para respeitar o que vem do arquivo de ambiente.
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        ////03:41 configuração de arquivo de ambiente para pegar de acordo com o que está funcionando
        //public IConfiguration Configuration { get; }
        #endregion

        public IConfiguration Configuration { get; }

        //Se for ambiente de desenvolvimento le o enviroment de desenovlimento caso contrario le o de produção
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder() //Buider que retornar a estancia de configuration builder que é a interface de configuration, será criado uma configuração mais completa
                .SetBasePath(hostEnvironment.ContentRootPath) //Base path pega o caminho de onde a aplicação está ROOT, o rootPath
                .AddJsonFile("appsettings.json", true, true) //Apos pegar o root path descobre o arquivo .Json e adiciona na configuração 
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true) //So que será adiciona outro arquivo dependendo do ambiente onde você estiver, fazendo appsetting o EnviromentName que é o ambiente ponto o json
                .AddEnvironmentVariables(); 

            //Caso seja ambiente de desenvoolvimento e você não quiser colocar uma chave privada sua, pode usar a configuração do secrets 
            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

              
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentityConfiguration(Configuration); //Passando como parametro a interface de configuracao para tratar os dados que são precisos
            #region  Configuração feita em arquivo separado para respeita boas praticas
            //services.AddDbContext<ApplicationDbContext>(
            //options =>
            //options.UseSqlServer(Configuration.GetConnectionString("GeekShoppingIdentityContextConnection")));

            ////Configuração de suporte ao Identity             
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddRoles<IdentityRole>() //Adicionando as regras de perfil de usuario

            //    .AddErrorDescriber<IdentityMensagenPortugues>() //Implementa IdentityMensagenPortugues para sobre escrever os valores da classe, essa classe herda de IdentityErrorDescriber

            //    .AddEntityFrameworkStores<ApplicationDbContext>() //especificando que você vai trabalhar com eneity framework

            //    .AddDefaultTokenProviders(); //Token que são gerados, criptografia para reconhecimento e autenticação 

            //#region //JWT

            //var appSettingsSection = Configuration.GetSection("AppSettings"); //Pegou o nó do arquivo de configuração appSettings
            //services.Configure<AppSettings>(appSettingsSection); //Configure o appsetting atraves do appseting section, configurando no middlaware/pipelane
            //                                                     //para que a classe AppSettings represente os dados da sessão appSettingsSection vindo das configurações

            //var appSettings = appSettingsSection.Get<AppSettings>(); //Obtem a AppSettings já populada
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret); //Transforma a chave do token em uma sequencia de bytes para uso mais adiante

            ////O padrão de autenticação depende da forma de implementação
            ////neste caso o privider de autenticação que está sendo utilizado é AuthenticationScheme do Json web Token
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(bearerOptions => //quando você usa AddJwtBearer, você está dizendo que está adicionado suporte para este tipo especifico de token

            //{
            //    bearerOptions.RequireHttpsMetadata = true; //requerer acesso pelo https por segurança 
            //    bearerOptions.SaveToken = true; //dizer que o token vai ser guardado na estanci assim que o login for realizado com sucesso
            //    bearerOptions.TokenValidationParameters = new TokenValidationParameters //parametro de validação do token logo a baixo
            //    {
            //        ValidateIssuerSigningKey = true, //Audiencia quer dizer onde o token pode ser utilizado, pode ser utilziado em dominio X ou Y, você vai validar com base na assinatura
            //        IssuerSigningKey = new SymmetricSecurityKey(key), //Assinatura do emissor criada a partida da classe SymmetricSecurityKey
            //        ValidateIssuer = true, //Issuer valido para que o token seja valida apenas para dentro das apis que você quiser, então sempre validar o emissor, não aceitar o token de outros emissores para as apis
            //        ValidateAudience = true, //Validar para quais dominios esse token é válido

            //        // ValidAudiences = appSettings.Emissor, //valida varios pontos de audiencia 

            //        ValidAudience = appSettings.ValidoEm, //Criar a audiencia de para onde o token será valido
            //        ValidIssuer = appSettings.Emissor //Issuer valido 

            //        //Caso não seja o mesmo token dentro da mesma audiencia o token não vai ser válido para api

            //    };
            //});

            #endregion

            services.AddApiConfiguration();
            #region Add Controller ficou em uma pasta separada so para o servico e foi configurado um servio so para ele 
            //services.AddControllers(); //Add Controller ficou em uma pasta separada so para o servico e foi configurado um servio so para ele 
            #endregion

            services.AddSwaggerConfiguration();
            #region //Extração das configuracoes do swagger para uma classe separada
            //services.AddSwaggerGen( c => {

            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    { 
            //     Title = "GeekShopping", //Titulo da sua api
            //     Description = "Esta API faz parte do GeekShopping ", //Descrição
            //     Contact = new OpenApiContact() { Name = "Pedro Cruz", Email = "pedrolucas11@live.com" },
            //     License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") } //licensa da api, pode ser colocado qualquer link

            //    });
            //});
            #endregion

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();
            #region configuracao de adição do swagger separado em classe para atender melhores praticas
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");            
            //});
            #endregion

            app.UseApiConfiguration(env);
            #region servico de api foi separado na sua proprio classe
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            ////else
            ////{
            ////    app.UseExceptionHandler("/Error");
            ////    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            ////    app.UseHsts();
            ////}

            //app.UseHttpsRedirection();
            ////app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();
            //app.UseAuthentication(); //adicionado para autenticar a aplicação

            ////Utilize os endpoint, as controller, rotas das controllers 
            //app.UseEndpoints(endpoints =>
            //{
            //    //endpoints.MapRazorPages();
            //    endpoints.MapControllers(); //vai criar um mapeamento das controllers 
            //});
            #endregion
        }
    }
}
