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
        private readonly HttpClient _httpClient;

        TServices _services = new TServices();
        public BillController(HttpClient httpClient)
        {
            _httpClient= httpClient;
        }
        public async Task<IActionResult> Index()
		{
			var vouchers = await _services.GetAll<Voucher>("https://localhost:7149/api/voucher");
			//var sttnill = await _services.GetAll<BillStatus>("");
			var product = await _services.GetAll<Bill>("https://localhost:7149/api/bill");
			List<User> account = await _services.GetAll<User>("https://localhost:7149/api/User");
			var billstatus = await _services.GetAll<BillStatus>("https://localhost:7149/api/billstatus");
            ViewData["voucher"] = vouchers;
            ViewData["Account"] = account;
            ViewData["billstatus"] = billstatus;
            return View(product);

        }

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)//Mở form
		{
			var product = await _services.GetAll<Bill>($"https://localhost:7149/api/bill");
			var billid =  product.FirstOrDefault(b => b.Id == id);
            var billstatus = await _services.GetAll<BillStatus>("https://localhost:7149/api/billstatus");
            ViewData["billstatus"] = billstatus;
            return View(billid);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(Bill bill)
		{
			var result =  await _httpClient.PutAsJsonAsync($"https://localhost:7149/api/bill/{bill.Id}", bill);
			return RedirectToAction("Index","Bill","Admin");
		}
	}
}
