using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pro131_Nhom4.IService;
using Pro131_Nhom4.Services;
using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/billdt")]
    [ApiController]
    public class BillDetailsController : ControllerBase
    {
        private readonly IBillDetailsService _ibilldt;
        public BillDetailsController(IBillDetailsService ibilldt)
        {
            _ibilldt = ibilldt;
        }
        [HttpGet]
        public async Task<IActionResult> ShowListBillDetails()
        {
            var billdt = await _ibilldt.GetAllBillDetails();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(billdt, settings);
            JToken parsedJson = JToken.Parse(json);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            return Ok(formattedJson);
        }
        [HttpPost]
        public async Task<ActionResult<BillDetails>> CreateBillDetails(BillDetails billDetails)
        {
            await _ibilldt.CreateBillDetails(billDetails);
            return Ok(billDetails);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<BillDetailsView>> UpdateBillDetails(BillDetailsView billDetails)
        {
            await _ibilldt.UpdateBillDetails(billDetails);
            return Ok(billDetails);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BillDetailsView>> DeleteBillDetails([FromRoute] Guid id)
        {
            await _ibilldt.DeleteBillDetails(id);
            return Ok();
        }
    }
}
