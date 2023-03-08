using GeekShoppingApp.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShoppingApp.Identity.Inicializer
{
    //Classe usada para popular o banco de dados.
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            string papelAdmin = "Admins";
            string papelClient = "Client";

            if (_roleManager.FindByNameAsync(papelAdmin).Result != null) return;
            _roleManager.CreateAsync(new IdentityRole(papelAdmin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(papelClient)).GetAwaiter().GetResult();


            #region //-INICIO DE CRIAÇÃO DO ADMIN------------------------------------------------------
            IdentityUser identityUserAdmin = new IdentityUser()
            {
                UserName = "Admin",
                Email = "Admin@Admin.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11) 123456789"        
                //Faltacolocar o FirstName Aqui
                //FaltaColocar o LastName Aqui

            };

            //Adicionando o usuário criado ao banco na inicialização
            _userManager.CreateAsync(identityUserAdmin, "Admins123@").GetAwaiter().GetResult();
            //Amarrando o usuário criado a um papel na inicialização
            _userManager.AddToRoleAsync(identityUserAdmin, papelAdmin).GetAwaiter().GetResult();

            //Adiciona as claims para o usuario passado por parametro no metodo AddClaimsAsync
            var adminClaim = _userManager.AddClaimsAsync(identityUserAdmin, new Claim[]
            {
            new Claim("Role","Admin")
                
            
            }).Result;
                    
      
            #endregion //-FIM DE CRIAÇÃO DO ADMIN------------------------------------------------------    

            #region //-INICIO DE CRIAÇÃO DO ClIENT------------------------------------------------------
            IdentityUser identityUserClient = new IdentityUser()
            {
                UserName = "Client",
                Email = "Client@Client.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11) 123456789"
                //Faltacolocar o FirstName Aqui
                //FaltaColocar o LastName Aqui

            };

            //Adiciona as claims para o usuario passado por parametro no metodo AddClaimsAsync
            var ClientClaim = _userManager.AddClaimsAsync(identityUserClient, new Claim[]
            {
            new Claim("Role","Client")


            }).Result;

            //Adicionando o usuário criado ao banco na inicialização
            _userManager.CreateAsync(identityUserClient, "Client123@").GetAwaiter().GetResult();
            //Amarrando o usuário criado a um papel na inicialização
            _userManager.AddToRoleAsync(identityUserClient, papelClient).GetAwaiter().GetResult();
            #endregion  //-FIM DE CRIAÇÃO DO ClIENT------------------------------------------------------    

        }
    }
}
