using App_Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/voucher")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVoucher()
        {
            var getall = await _voucherService.GetAllVoucher();
            return Ok(getall);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetVoucherById(Guid id)
        {
            var getid = await _voucherService.GetVoucherById(id);
            return Ok(getid);
        }
        // POST api/<VoucherController>
        [HttpPost]
        public async Task<ActionResult> CreateVoucher(Voucher voucher) ///mở form
        {
            await _voucherService.CreateVoucher(voucher);
            return Ok(voucher);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Voucher>> UpdateVoucher(Voucher voucher)
        {
            await _voucherService.UpdateVoucher(voucher);
            return Ok(voucher);
        }

        [HttpGet("{Name}")]
        public async Task<IActionResult> GetVoucherByName(string name)
        {
            var getname = await _voucherService.GetVoucherByName(name);
            return Ok(getname);
        }


        // DELETE api/<VoucherController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVoucher(Guid id)
        {
            var del = _voucherService.DeleteVoucher(id);
            return Ok(del);
        }
    }
}
