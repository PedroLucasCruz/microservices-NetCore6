using GeekShoppingApp.Identity.Controllers;
using GeekShoppingClient.Web.Models;
using GeekShoppingClient.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoppingClient.Web.Controllers
{
  
    public class IdentidadeController : MainController
    {

        private readonly IAutenticacaoService _autenticacaoService;

        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }


        [HttpPost("identidade/autenticar")]
        public async Task<IActionResult> login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return  CustomResponse(await _autenticacaoService.Login(usuarioLogin));
        }


    }
}
