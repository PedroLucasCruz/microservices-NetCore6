using GeekShoppingClient.Web.Models;

namespace GeekShoppingClient.Web.Services.IServices
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLoginModel> Login(UsuarioLogin usuarioLogin);
        Task<string> Registro(UsuarioRegistro usuarioRegistro);
        bool EstaAutenticado();
    }
}
