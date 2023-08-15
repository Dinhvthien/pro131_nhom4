using App_client.Services;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using App_Shared.Model;
using Pro131_Nhom4.Data;

namespace App_client.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BillController : Controller
	{
		TServices _services = new TServices();
		public async Task<IActionResult> Index()
		{
			var vouchers = await _services.GetAll<Voucher>("https://localhost:7149/api/voucher");
			//var sttnill = await _services.GetAll<BillStatus>("");
			var product = await _services.GetAll<Bill>("https://localhost:7149/api/bill");
			var account = await _services.GetAll<User>("https://localhost:7149/api/User");
			return View(product);
            ViewData["voucher"] = vouchers;
			ViewData["Account"] = account;
        }
	}
}
