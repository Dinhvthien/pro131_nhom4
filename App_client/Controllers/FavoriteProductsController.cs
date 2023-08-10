using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.Data;
using System.Security.Claims;

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

		public IActionResult Create()
		{
			return View();
		}
		public async Task<IActionResult> Creating(CreateFavoriteProducts rq, Guid id)
		{
            var GetIdLogin = HttpContext.Session.GetString("IdLogin");
            rq.AccountID = Guid.Parse(GetIdLogin);
            rq.ProductID = id;
            rq.Description = "Yêu thích";
            var result = await _services.CreateAll<CreateFavoriteProducts>("https://localhost:7149/api/CRUDFavoritePr/Create", rq);
			if (result)
			{
				return RedirectToAction("Index");
			}
			return View();
		}



        public async Task<IActionResult> Delete(Guid idproduct)
        {
            var GetIdLogin = HttpContext.Session.GetString("IdLogin");
            var a = await _services.Delete_DungBM_2id("https://localhost:7149/api/CRUDFavoritePr/Delete", Guid.Parse(GetIdLogin), idproduct);
            if (a == 0)
            {
                ViewData["XoaThatBai"] = "Xóa thất bại";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["XoaThanhCong"] = "Xóa tHành công";
                return RedirectToAction("Index");
            }

        }
        
    }
}
