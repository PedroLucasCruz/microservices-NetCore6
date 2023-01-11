using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GeekShoppingClient.Web.Utils
{
    static public class HttpClienExtensions
    {
        

        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
        public static async Task<T> ReadContentAs<T>(
            this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Somenthing went sront calling the API: "+ $"{ response.ReasonPhrase}");
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            JsonSerializerOptions option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(dataAsString, option);
        }
        
        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
  
            return   httpClient.PostAsJsonAsync(url, TratarSerialize(data));
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
         
            return httpClient.PutAsJsonAsync(url, TratarSerialize(data));
        }

        //Get com parametro
        public static Task<HttpResponseMessage> GetAsync<T>(this HttpClient httpClient, string url, T data)
        {          
            return httpClient.GetAsync(url, TratarSerialize(data));
        }

        //Get sem parametro
        public static Task<HttpResponseMessage> GetAsync<T>(this HttpClient httpClient, string url)
        {
            return httpClient.GetAsync(url);
        }

        private static StringContent TratarSerialize<T>(T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = contentType;

            return content;
        }
    }
}

