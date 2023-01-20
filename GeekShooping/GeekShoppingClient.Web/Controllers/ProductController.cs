using GeekShoppingClient.Web.Models;
using GeekShoppingClient.Web.Service.IServices;
using GeekShoppingClient.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoppingClient.Web.Controllers
{
   
   
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAutenticacaoService _autenticacaiService;

        public ProductController(IProductService productService, IAutenticacaoService autenticacaoService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _autenticacaiService = autenticacaoService ?? throw new ArgumentNullException(nameof(autenticacaoService));
        }

        [HttpGet]
        //[Authorize]
        [Route("product/GetAll")] 
        public async Task<IActionResult> getAll()
        {
            var retorno = _autenticacaiService.EstaAutenticado();

            if (!ModelState.IsValid) return BadRequest("Model Inválida");

            IEnumerable<ProductModel> products = await _productService.FindAllProducts();
            return Ok(products);
        }

        public async Task<IActionResult> FindProductById([FromQuery] long Id)
        {
            if (!ModelState.IsValid) return BadRequest("Model Inválida");

            ProductModel products = await _productService.FindProductById(Id);
            return Ok(products);
        }

        public async Task<IActionResult> CreateProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid) return BadRequest("Model Inválida");

            ProductModel products = await _productService.CreateProduct(productModel);
                return Ok(products);
        }

        public async Task<IActionResult> UpdateProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid) return BadRequest("Model Inválida");
            ProductModel products = await _productService.UpdateProduct(productModel);
            return Ok(products);
        }

        public async Task<IActionResult> DeleteProduct([FromQuery] long Id)
        {
            if (!ModelState.IsValid) return BadRequest("Model Inválida");

            bool products = await _productService.Delete(Id);
            return Ok(products);
        }

    }
}
