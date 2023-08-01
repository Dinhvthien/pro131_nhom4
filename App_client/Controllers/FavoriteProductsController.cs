using App_client.Services;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App_client.Controllers
{
    public class FavoriteProductsController : Controller
    {
        private readonly ILogger<FavoriteProductsController> _logger;
        private readonly TServices _services;

        public FavoriteProductsController(ILogger<FavoriteProductsController> logger)
        {
            _services = new TServices();
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAll<ViewFavoriteProduct>("https://localhost:7149/api/CRUDFavoritePr/GetAll/");
            return View(result);
        }
    }
}
