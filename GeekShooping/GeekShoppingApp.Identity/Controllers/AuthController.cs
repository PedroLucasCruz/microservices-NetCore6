using GeekShoppingApp.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace GeekShoppingApp.Identity.Controllers
{
    [ApiController] //Usado para liberar os entendimento dos schemas do swagger, decorando com está anotação você esta dizendo que ela é uma api controller, o schemas trafegam json e não formulario
    [Route("api/identidade")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return BadRequest(); //Validação da model de registro de usuário
            var user = new IdentityUser //Estancia do identity recebendo no UserName o Email para que o usuario utilze o email para autenticação no usuario
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(); //Se funcionar chegando nesta linha retorna 200Ok sucesso 
            }

            return BadRequest(); //Caso o result succeded não funcione retorna 
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return BadRequest(); //Validação da model de registro de usuário

            var result = await _signInManager.PasswordSignInAsync(
                usuarioLogin.Email, //Parametro para validar pelo Email/Usuario
                usuarioLogin.Senha, //Parametro para  validar pela senha 
                false, //parametro para dizer se o usuario é persistente 
                true //parametro para bloquear o usuário caso faça muitas tentativas de login, desbloqueia o usuario depois de 5min
                );

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();

        }

    }     
    
}
