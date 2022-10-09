#region using necess�rio dos namespaces da aplica��o
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
        ////03:41 configura��o de arquivo de ambiente para pegar de acordo com o que est� funcionando
        //public IConfiguration Configuration { get; }
        #endregion

        public IConfiguration Configuration { get; }

        //Se for ambiente de desenvolvimento le o enviroment de desenovlimento caso contrario le o de produ��o
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder() //Buider que retornar a estancia de configuration builder que � a interface de configuration, ser� criado uma configura��o mais completa
                .SetBasePath(hostEnvironment.ContentRootPath) //Base path pega o caminho de onde a aplica��o est� ROOT, o rootPath
                .AddJsonFile("appsettings.json", true, true) //Apos pegar o root path descobre o arquivo .Json e adiciona na configura��o 
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true) //So que ser� adiciona outro arquivo dependendo do ambiente onde voc� estiver, fazendo appsetting o EnviromentName que � o ambiente ponto o json
                .AddEnvironmentVariables(); 

            //Caso seja ambiente de desenvoolvimento e voc� n�o quiser colocar uma chave privada sua, pode usar a configura��o do secrets 
            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

              
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentityConfiguration(Configuration); //Passando como parametro a interface de configuracao para tratar os dados que s�o precisos
            #region  Configura��o feita em arquivo separado para respeita boas praticas
            //services.AddDbContext<ApplicationDbContext>(
            //options =>
            //options.UseSqlServer(Configuration.GetConnectionString("GeekShoppingIdentityContextConnection")));

            ////Configura��o de suporte ao Identity             
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddRoles<IdentityRole>() //Adicionando as regras de perfil de usuario

            //    .AddErrorDescriber<IdentityMensagenPortugues>() //Implementa IdentityMensagenPortugues para sobre escrever os valores da classe, essa classe herda de IdentityErrorDescriber

            //    .AddEntityFrameworkStores<ApplicationDbContext>() //especificando que voc� vai trabalhar com eneity framework

            //    .AddDefaultTokenProviders(); //Token que s�o gerados, criptografia para reconhecimento e autentica��o 

            //#region //JWT

            //var appSettingsSection = Configuration.GetSection("AppSettings"); //Pegou o n� do arquivo de configura��o appSettings
            //services.Configure<AppSettings>(appSettingsSection); //Configure o appsetting atraves do appseting section, configurando no middlaware/pipelane
            //                                                     //para que a classe AppSettings represente os dados da sess�o appSettingsSection vindo das configura��es

            //var appSettings = appSettingsSection.Get<AppSettings>(); //Obtem a AppSettings j� populada
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret); //Transforma a chave do token em uma sequencia de bytes para uso mais adiante

            ////O padr�o de autentica��o depende da forma de implementa��o
            ////neste caso o privider de autentica��o que est� sendo utilizado � AuthenticationScheme do Json web Token
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(bearerOptions => //quando voc� usa AddJwtBearer, voc� est� dizendo que est� adicionado suporte para este tipo especifico de token

            //{
            //    bearerOptions.RequireHttpsMetadata = true; //requerer acesso pelo https por seguran�a 
            //    bearerOptions.SaveToken = true; //dizer que o token vai ser guardado na estanci assim que o login for realizado com sucesso
            //    bearerOptions.TokenValidationParameters = new TokenValidationParameters //parametro de valida��o do token logo a baixo
            //    {
            //        ValidateIssuerSigningKey = true, //Audiencia quer dizer onde o token pode ser utilizado, pode ser utilziado em dominio X ou Y, voc� vai validar com base na assinatura
            //        IssuerSigningKey = new SymmetricSecurityKey(key), //Assinatura do emissor criada a partida da classe SymmetricSecurityKey
            //        ValidateIssuer = true, //Issuer valido para que o token seja valida apenas para dentro das apis que voc� quiser, ent�o sempre validar o emissor, n�o aceitar o token de outros emissores para as apis
            //        ValidateAudience = true, //Validar para quais dominios esse token � v�lido

            //        // ValidAudiences = appSettings.Emissor, //valida varios pontos de audiencia 

            //        ValidAudience = appSettings.ValidoEm, //Criar a audiencia de para onde o token ser� valido
            //        ValidIssuer = appSettings.Emissor //Issuer valido 

            //        //Caso n�o seja o mesmo token dentro da mesma audiencia o token n�o vai ser v�lido para api

            //    };
            //});

            #endregion

            services.AddApiConfiguration();
            #region Add Controller ficou em uma pasta separada so para o servico e foi configurado um servio so para ele 
            //services.AddControllers(); //Add Controller ficou em uma pasta separada so para o servico e foi configurado um servio so para ele 
            #endregion

            services.AddSwaggerConfiguration();
            #region //Extra��o das configuracoes do swagger para uma classe separada
            //services.AddSwaggerGen( c => {

            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    { 
            //     Title = "GeekShopping", //Titulo da sua api
            //     Description = "Esta API faz parte do GeekShopping ", //Descri��o
            //     Contact = new OpenApiContact() { Name = "Pedro Cruz", Email = "pedrolucas11@live.com" },
            //     License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") } //licensa da api, pode ser colocado qualquer link

            //    });
            //});
            #endregion

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();
            #region configuracao de adi��o do swagger separado em classe para atender melhores praticas
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
            //app.UseAuthentication(); //adicionado para autenticar a aplica��o

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
