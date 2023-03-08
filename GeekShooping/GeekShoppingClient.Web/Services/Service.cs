using GeekShoppingClient.Web.Extensions;

namespace GeekShoppingClient.Web.Services
{
    public  abstract class Service
    {
        protected bool TratarErrosResponse(HttpResponseMessage response)
        {

            //Se passar deste case quer dizer que foi um processo de sucesso na aplicação
            //Tratamento de todos os código de erro é feito aqui antes
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);
                case 400: //Quando tenho um erro 400 significa que tenho erros dentro do response 
                    return false;
            }

            //Case seja um código que não represente um código de sucesso, ao cair na linha do EnsureSuccessStatusCode
            //ao cair na linha do EnsureSuccessStatusCode estoura Exception quando o codigo não é de sucesso
            //Se chegar até tem que ser codigo de sucesso, caso contrario da Exception.
            response.EnsureSuccessStatusCode();
            return true; //retornou true por que está tudo certo.


        }
    }
}
