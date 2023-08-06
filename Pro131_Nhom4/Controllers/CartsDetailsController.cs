using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/cartdt")]
    [ApiController]
    public class CartsDetailsController : ControllerBase
    {
        private readonly ICartDetailsService _cartDetailsService;
        public CartsDetailsController(ICartDetailsService cartDetailsService)
        {
            _cartDetailsService = cartDetailsService;
        }
        [HttpGet]
        public async Task<IActionResult> ShowListCartDetails()
        {
            var cartdt = await _cartDetailsService.GetAllCartDetails();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(cartdt, settings);
            JToken parsedjson = JToken.Parse(json);
            string formattedJson = parsedjson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCartDetailsByAccountId([FromRoute] Guid id)
        {
            var cartdt = await _cartDetailsService.GetCartDetailsByAccountId(id);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(cartdt, settings);
            JToken parsedjson = JToken.Parse(json);
            string formattedJson = parsedjson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }
        [HttpGet("GetById/{accountId:Guid}/{productId:Guid}")]
        public async Task<IActionResult> GetCartDetailsById(Guid accountId, Guid productId)
        {
            var cartdt = await _cartDetailsService.GetCartDetailsById(accountId, productId);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(cartdt, settings);
            JToken parsedjson = JToken.Parse(json);
            string formattedJson = parsedjson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }

        [HttpPost]
        public async Task<ActionResult<CartDetails>> CreateCartDetails(CartDetails cart)
        {
            await _cartDetailsService.CreateCartDetails(cart);
            return Ok(cart);
        }

        [HttpPut("Increase/{id}")]
        public async Task<ActionResult<CartDetails>> Increase(Guid id)
        {
            await _cartDetailsService.Increase(id);
            return Ok();
        }

        [HttpPut("Reduce/{id}")]
        public async Task<ActionResult<CartDetails>> Reduce(Guid id)
        {
            await _cartDetailsService.Reduce(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CartDetailsView>> DeleteCartDetails(Guid id)
        {
            await _cartDetailsService.DeleteCartDetails(id);
            return Ok();
        }
    }
}
