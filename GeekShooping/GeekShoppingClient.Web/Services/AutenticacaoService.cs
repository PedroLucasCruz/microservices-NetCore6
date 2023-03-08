using GeekShoppingClient.Web.Models;
using GeekShoppingClient.Web.Services.IServices;
using GeekShoppingClient.Web.Utils;
using System.Text;
using System.Text.Json;

namespace GeekShoppingClient.Web.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        //private readonly IHttpContextAccessor _acessor;
        private readonly HttpClient _httpClient;
           
        public const string BasePath = "api/identidade/Auth/";

        public AutenticacaoService(
            HttpClient httpClient
            //IHttpContextAccessor acessor
            )
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            //_acessor = acessor?? throw new ArgumentNullException(nameof(acessor));
        }

        public async Task<UsuarioRespostaLoginModel> Login(UsuarioLogin usuarioLogin)
        {
        
            var response = await _httpClient.PostAsJson(BasePath + "autenticar", usuarioLogin);

            //Tratar erro mais tarde no retorno
            //if (!TratarErrosResponse(response))
            //{
            //    return new UsuarioRespostaLoginModel
            //    {
            //        ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
            //    }
            //}


            if (response.IsSuccessStatusCode) //se o status for diferente de 200 ou 201 ou qualquer status de sucesso retorna execption
                return await response.ReadContentAs<UsuarioRespostaLoginModel>();
            else throw new Exception("Somenthing went wrong when calling API");

        }

        public async Task<UsuarioRespostaLoginModel> Registro(UsuarioRegistro usuarioRegistro)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(usuarioRegistro), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BasePath + "nova-conta", loginContent);

            return JsonSerializer.Deserialize<UsuarioRespostaLoginModel>(await response.Content.ReadAsStringAsync());
        }

        public bool EstaAutenticado()
        {
            throw new NotImplementedException();
        }

        public async void Logout()
        {
          await _httpClient.GetAsync(BasePath + "deslogar");

        }
    }
}
