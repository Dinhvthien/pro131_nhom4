using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pro131_Nhom4.IService;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _services;
        public CartsController(ICartService cartService)
        {
            _services = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> ShowListCart()
        {
            var cart = await _services.GetAllCart();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(cart, settings);
            JToken parsedJson = JToken.Parse(json);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            return Ok(formattedJson);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCartById([FromRoute] Guid id)
        {
            var cart = await _services.GetCartById(id);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(cart, settings);
            JToken parsedJson = JToken.Parse(json);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> Create(Cart cart)
        {
            await _services.CreateCart(cart);
            return Ok(cart);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cart>> Delete(Guid id)
        {
            await _services.DeleteCart(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CartView>> Update(CartView cartview)
        {
            await _services.UpdateCart(cartview);
            return Ok(cartview);
        }

    }
}
