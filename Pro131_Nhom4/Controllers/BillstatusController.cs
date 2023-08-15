using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/billstatus")]
    [ApiController]
    public class BillstatusController : ControllerBase
    {
        private readonly IBillStatusServices _billStatus;
        public BillstatusController(IBillStatusServices billStatus)
        {
            _billStatus = billStatus;
        }
        [HttpGet]
        public async Task<IActionResult> ShowListBillDetails()
        {
            var billdt = await _billStatus.GetAllBillStatusAsync();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(billdt, settings);
            JToken parsedJson = JToken.Parse(json);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }
    }
}
