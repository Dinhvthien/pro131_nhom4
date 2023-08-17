using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/showlist")]
    [ApiController]
   
    public class ListProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ListProductsController(IProductService productService)
        {
            _productService = productService;
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ShowListProduct()
        {
            var product = await _productService.ShowListProduct();
            return Ok(product);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(product);
        }

        [HttpGet("{name}/{colorId:Guid}/{sizeId:Guid}")]
        public async Task<IActionResult> GetProductByCS(string name, Guid colorId, Guid sizeId)
        {
            var product = await _productService.GetProductByCS(name, colorId, sizeId);
            return Ok(product);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductByName([FromRoute] string name)
        {
            var product = await _productService.GetProductByName(name);
            return Ok(product);
        }



        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await _productService.CreateProduct(product);
            return Ok(product);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<ProductView>> UpdateProduct(ProductView product)
        {
            await _productService.UpdateProduct(product);
            return Ok(product);
        }
		[HttpPut("test/{id}/{slsp}")]

		public async Task<ActionResult<Product>> UpdateProduct2(Guid id, int slsp)
		{

			await _productService.UpdateProduct2(id,slsp);
			return Ok();
		}

		[HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct([FromRoute] Guid id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
