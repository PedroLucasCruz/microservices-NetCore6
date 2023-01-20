using GeekShoppingClient.Web.Models;
using GeekShoppingClient.Web.Service.IServices;
using System.Text;
using System.Text.Json;

namespace GeekShoppingClient.Web.Service
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IHttpContextAccessor _acessor;
        private readonly HttpClient _httpClient;

        public AutenticacaoService(IHttpContextAccessor acessor)
        {
            _acessor = acessor?? throw new ArgumentNullException(nameof(acessor));
        }

        public async Task<string> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(usuarioLogin), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:5001/api/identidade/autenticar", loginContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());            
        }

        public async Task<string> Registro(UsuarioRegistro usuarioRegistro)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(usuarioRegistro), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:5001/api/identidade/nova-conta", loginContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
        
        public bool EstaAutenticado()
        {
            return _acessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
