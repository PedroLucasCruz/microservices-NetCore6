using GeekShoppingClient.Web.Models;
using GeekShoppingClient.Web.Service.IServices;

namespace GeekShoppingClient.Web.Service
{
    public class AutenticacaoService : IAutenticacaoService
    {
        public Task<string> Login(UsuarioLogin usuarioLogin)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> Registro(UsuarioRegistro usuarioRegistro)
        {
            throw new System.NotImplementedException();
        }       
    }
}
