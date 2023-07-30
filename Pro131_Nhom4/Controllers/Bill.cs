using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pro131_Nhom4.IService;
using App_Shared.Model;
using Pro131_Nhom4.Services;
using Microsoft.AspNetCore.Authorization;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/bill")]
    [ApiController]
    public class Bill : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IBillDetailsService _billDetailsService;
        private readonly IBillStatusServices _billStatusServices;
        private readonly IProductService _productService;
        private readonly ICartDetailsService _cartDetailsService;
        public Bill(IBillService billService, IBillDetailsService billDetailsService, IBillStatusServices billStatusServices, IProductService productService, ICartDetailsService cartDetailsService)
        {
            _billDetailsService = billDetailsService;
            _billService = billService;
            _billStatusServices = billStatusServices;
            _productService = productService;
            _cartDetailsService = cartDetailsService;
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ShowListBill()
        {
            var bill = await _billService.GetAllBills();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(bill, settings);
            JToken parsedJson = JToken.Parse(json);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }


      
    }
}
