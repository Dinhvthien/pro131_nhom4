using App_client.Models;
using App_client.Services;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App_client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        TServices _services = new TServices();
        public async Task<IActionResult> Index()
        {
            var product = await _services.GetAll<ProductView>("https://localhost:7149/api/showlist");
            var p = product.GroupBy(p => new { p.Name, p.ColorID }).Select(g => g.First()).ToList();
            return View(p);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}