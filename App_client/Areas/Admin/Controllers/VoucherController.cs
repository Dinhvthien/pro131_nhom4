using App_client.Services;
using App_Shared.Model;
using Microsoft.AspNetCore.Mvc;
using App_Shared.Model;
namespace App_client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VoucherController : Controller
	{
		TServices _services = new TServices();
		public async Task<IActionResult> Index()
		{
			var voucher = await _services.GetAll<Voucher>("https://localhost:7149/api/voucher");

			return View(voucher);
		}
	}
}
