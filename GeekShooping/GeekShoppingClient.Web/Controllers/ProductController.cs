using GeekShoppingApp.Identity.Controllers;
using GeekShoppingClient.Web.Models;
using GeekShoppingClient.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoppingClient.Web.Controllers
{
   
   
    public class ProductController : MainController
    {
        private readonly IProductService _productService;
      
        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
     
        }

        [Authorize]
        [HttpGet("product/GetAll")]
        public async Task<IActionResult> getAll()
        {
          

            if (!ModelState.IsValid) return BadRequest("Model Inválida");

            IEnumerable<ProductModel> products = await _productService.FindAllProducts();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("product/FindProductById/{Id}")]
        public async Task<IActionResult> FindProductById(long Id)
        {
            if (!ModelState.IsValid) return BadRequest("Model Inválida");

            ProductModel products = await _productService.FindProductById(Id);
            return Ok(products);
        }

        [Authorize]
        [HttpPost("product/CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid) return BadRequest("Model Inválida");

            ProductModel products = await _productService.CreateProduct(productModel);
                return Ok(products);
        }

        [Authorize]
        [HttpPut("product/CreateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid) return BadRequest("Model Inválida");
            ProductModel products = await _productService.UpdateProduct(productModel);
            return Ok(products);
        }

        [Authorize]
        [HttpDelete("product/DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProduct(long Id)
        {
            if (!ModelState.IsValid) return BadRequest("Model Inválida");

            bool products = await _productService.Delete(Id);
            return Ok(products);
        }

    }
}
