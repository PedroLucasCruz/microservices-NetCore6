using GeekShoppingClient.Web.Models;

namespace GeekShoppingClient.Web.Service.IServices
{
    public interface IAutenticacaoService
    {
        Task<string> Login(UsuarioLogin usuarioLogin);
        Task<string> Registro(UsuarioRegistro usuarioRegistro);
    }
}
