using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Utils
{
  static  public class HttpClienExtensions
    {
      private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
       public static async Task<T> ReadContentAs<T> (
           this HttpResponseMessage response)
        {
            if(!response.IsSuccessStatusCode) throw
                    new ApplicationException($"Somenthing went sront calling the API: "
                    + $"{ response.ReasonPhrase}");
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            JsonSerializerOptions option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(dataAsString, option);
        }
        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsJsonAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsJsonAsync(url, content);
        }
    }
}

