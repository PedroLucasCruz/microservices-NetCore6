using GeekShoppingApp.Identity.Controllers;
using GeekShoppingClient.Web.Configurations.IConfig;
using GeekShoppingClient.Web.Extensions;
using GeekShoppingClient.Web.Models;
using GeekShoppingClient.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeekShoppingClient.Web.Controllers
{
  
    [Route("identidade")]
    public class IdentidadeController : MainController
    {
        //Continuar da aula M04V04 - Models, Views e Controllers de login nos 01:44 min
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly IJwtConfig _jwtConfig;
        private readonly IUser _user;

        public IdentidadeController(
            IAutenticacaoService autenticacaoService,
            IJwtConfig jwtConfig,
            IUser user
            )
        {
            _autenticacaoService = autenticacaoService;
            _jwtConfig = jwtConfig;
            _user = user;
        }
             
        [HttpPost("autenticar")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resposta = await _autenticacaoService.Login(usuarioLogin);

            await RealizarLoginClient(resposta);

            return  CustomResponse(resposta);
        }

        [HttpPost("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resposta = await _autenticacaoService.Registro(usuarioRegistro);

            await RealizarLoginClient(resposta);

            return CustomResponse(resposta);
        }

        [HttpPost("sair")]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            //Desloga o usuario la na camada do Identity 
            _autenticacaoService.Logout(); //retorna vazio do identity

            //Metodo para deslogar o usuário do contexto
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);


            return CustomResponse();
        }

        [HttpGet("EstaAutenticado")]
        public async Task<IActionResult> EstaAutenticado()
        {
            return CustomResponse(await _user.EstaAutenticadoAsync());      
        }

        [HttpGet("ObterUserEmail")]
        public async Task<IActionResult> ObterUserEmail()
        {
            return CustomResponse(await _user.ObterUserEmailAsync());
        }

        private async Task RealizarLoginClient(UsuarioRespostaLoginModel resposta)
        {
            var token = _jwtConfig.ObterTokenFormatado(resposta.AcessToken);

            //Adicionar as claims retornadas do serviço
            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", resposta.AcessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //var authProperties = new AuthenticationProperties
            //{
            //    AllowRefresh = true,
            //    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60), //Quanto tempo ele vai durar baseado no sistema de contagem universal
            //    IsPersistent = true, //Se ele é persistente, por que não vai durar apenas um request vai durar multiplos requests dentro do periodo de 60 minutos
            //    IssuedUtc = DateTime.UtcNow
            //};

            //Configuração Usando Cookie para autenticar
            //await HttpContext.SignInAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme,
            //    new ClaimsPrincipal(claimsIdentity),
            //    authProperties);

            ///Configuração usando JwtBearer Token 
            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,
                              new ClaimsPrincipal(claimsIdentity),
                              new AuthenticationProperties
                              {
                                  AllowRefresh = true,
                                  ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                                  IsPersistent = true,
                                  IssuedUtc = DateTime.UtcNow
                              });
        }
    }
}
