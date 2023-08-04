using App_client.Services;
using App_Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace App_client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RankController : Controller
    {
        TServices _services = new TServices();
        public async Task<IActionResult> Index()
        {
            var voucher = await _services.GetAll<Rank>("https://localhost:7149/api/Rank");
            return View(voucher);
        }
    }
}
