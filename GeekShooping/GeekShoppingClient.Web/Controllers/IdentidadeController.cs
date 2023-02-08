using GeekShoppingApp.Identity.Controllers;
using GeekShoppingClient.Web.Models;
using GeekShoppingClient.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoppingClient.Web.Controllers
{
  
    [Route("identidade")]
    public class IdentidadeController : MainController
    {
        //Continuar da aula M04V04 - Models, Views e Controllers de login nos 01:44 min
        private readonly IAutenticacaoService _autenticacaoService;

        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }


        [HttpPost("/autenticar")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return  CustomResponse(await _autenticacaoService.Login(usuarioLogin));
        }

        [HttpPost("/nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _autenticacaoService.Registro(usuarioRegistro));
        }

        [HttpPost("/sair")]
        public async Task<IActionResult> Logout(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _autenticacaoService.Logout(); //retorna vazio do identity
            
            return CustomResponse();
        }


    }
}
