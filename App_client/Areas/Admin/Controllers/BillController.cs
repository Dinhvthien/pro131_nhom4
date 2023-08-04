using App_client.Services;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using App_Shared.Model;
namespace App_client.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BillController : Controller
	{
		TServices _services = new TServices();
		public async Task<IActionResult> Index()
		{
			var product = await _services.GetAll<Bill>("https://localhost:7149/api/bill");
			return View(product);
		}
	}
}
