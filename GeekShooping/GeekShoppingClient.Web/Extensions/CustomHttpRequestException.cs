using System.Net;

namespace GeekShoppingClient.Web.Extensions
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode statusCode;
        public CustomHttpRequestException() { }

        public CustomHttpRequestException(string message, Exception innerException) : base(message, innerException) { }
   
        public CustomHttpRequestException(HttpStatusCode statusCode)
        {
            statusCode = statusCode;
        }    
    }
}
