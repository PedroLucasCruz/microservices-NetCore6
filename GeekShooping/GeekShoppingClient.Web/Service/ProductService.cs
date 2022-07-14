using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
            throw new NotImplementedException();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _httpClient.GetAsync($"{ BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
            throw new NotImplementedException();
        }

        public async Task<ProductModel> FindProductByName(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _httpClient.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode) //se o status for diferente de 200 ou 201 ou qualquer status de sucesso retorna execption
                return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Somenthing went wrong when calling API");
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _httpClient.PutAsJson(BasePath, model);
            if (response.IsSuccessStatusCode) //se o status for diferente de 200 ou 201 ou qualquer status de sucesso retorna execption
                return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Somenthing went wrong when calling API");
        }

        public async Task<bool> Delete(long id)
        {
            var response = await _httpClient.DeleteAsync($"{ BasePath}/{id}");
            if (response.IsSuccessStatusCode) //se o status for diferente de 200 ou 201 ou qualquer status de sucesso retorna execption
                return await response.ReadContentAs<bool>();
            else throw new Exception("Somenthing went wrong when calling API");
        }
    }
}
