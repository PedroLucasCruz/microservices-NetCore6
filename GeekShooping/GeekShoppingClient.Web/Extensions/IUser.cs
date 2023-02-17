using System.Security.Claims;

namespace GeekShoppingClient.Web.Extensions
{
    public interface IUser
    {
        string Name { get; }
        Guid ObterUserId();
        string ObterUserEmail();
        Task<string> ObterUserEmailAsync();
        string ObterUseToken();
        bool EstaAutenticado();
        Task<bool> EstaAutenticadoAsync();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
        HttpContext ObterHttpContext();
    }

    public class CoreUser : IUser
    {
        private readonly IHttpContextAccessor _acessor;

        public CoreUser(IHttpContextAccessor acessor)
        {
            _acessor = acessor;                      
        }

        public string Name => _acessor.HttpContext.User.Identity.Name;


        public Guid ObterUserId()
        {
            //Esse GetUserEmail foi implementado la em baixo no ClaimsPrincipalExtensions ao injetar ClaimsPrincipal
            return EstaAutenticado() ? Guid.Parse(_acessor.HttpContext.User.GetUserEmail()) : Guid.Empty;
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? _acessor.HttpContext.User.GetUserEmail() : "";
        }

        public async Task<string> ObterUserEmailAsync()
        {
            return await Task.Run(() => { return EstaAutenticado() ? _acessor.HttpContext.User.GetUserEmail() : ""; });
          
        }

        public string ObterUseToken()
        {
            return EstaAutenticado() ? _acessor.HttpContext.User.GetUserToken() : "";
        }

        public  bool EstaAutenticado()
        {
            return  _acessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task<bool> EstaAutenticadoAsync()
        {
            //converte um método sincrono para asyncrono na aplicação 
            return await Task.Run(() => { return _acessor.HttpContext.User.Identity.IsAuthenticated; }); 
        }

        public bool PossuiRole(string role)
        {
            return _acessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _acessor.HttpContext.User.Claims;
        }

        public HttpContext ObterHttpContext()
        {
            return _acessor.HttpContext;
        }        
    }


    public static class ClaimsPrincipalExtensions {

        //Sobre escrevendo a o ClaimsPrincipa por injeção no metodo usando This pra não ter que escrever toda a linha de injção separada e busca o objeto
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null) {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst("sub");
            return claim?.Value;

        }


        //Sobre escrevendo a o ClaimsPrincipa por injeção no metodo usando This pra não ter que escrever toda a linha de injção separada e busca o objeto
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst("email");
            return claim?.Value;

        }


        //Sobre escrevendo a o ClaimsPrincipa por injeção no metodo usando This pra não ter que escrever toda a linha de injção separada e busca o objeto
        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst("JWT");
            return claim?.Value;

        }



    }


}
