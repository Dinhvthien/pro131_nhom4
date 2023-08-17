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
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
			var userName = userIdClaim.Value;
			var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
			var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
			//Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdLogin"));
			var result = await _services.GetAll<ViewFavoriteProduct>("https://localhost:7149/api/CRUDFavoritePr/GetAll");
            var productfavorite = result.Where(c=>c.AccountID == userId);
            return View(result);
        }

		public async Task<IActionResult> topsp() {
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
			{
                return RedirectToAction("Index", "home");
            }
			else
			{
				var userName = userIdClaim.Value;
				var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
				var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
				var results = await _services.GetAll<ViewFavoriteProduct>("https://localhost:7149/api/CRUDFavoritePr/GetAll");
				var productfavorite = results.FindAll(c => c.AccountID == userId);
				ViewData["yeuthich"] = productfavorite;


				var result = await _services.GetAll<FavoriteProducts>("https://localhost:7149/api/CRUDFavoritePr/GetAll");
				var resutss = result.GroupBy(p => new { p.ProductID }).Select(g => g.First()).ToList();


				var productList = await _services.GetAll<Product>("https://localhost:7149/api/showlist");
				var filteredProducts = productList.Where(p => resutss.Any(r => r.ProductID == p.Id)).ToList();

				ViewData["product"] = filteredProducts;
				return View(result);
			}

		}



		public IActionResult Create()
		{
			return View();
		}
		public async Task<IActionResult> Creating(CreateFavoriteProducts rq, Guid id)
		{
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
			var userName = userIdClaim.Value;
			
			var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
			var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;

            rq.AccountID = userId;

			rq.ProductID = id;
            rq.Description = "Yêu thích";
            var result = await _services.CreateAll<CreateFavoriteProducts>("https://localhost:7149/api/CRUDFavoritePr/Create", rq);
			if (result)
			{
				return RedirectToAction("Index", "Product");
			}
			return RedirectToAction("Index", "home");
		}



        public async Task<IActionResult> Delete(Guid id)
        {
            var GetIdLogin = HttpContext.Session.GetString("IdLogin");
            var a = await _services.Delete_DungBM_2id("https://localhost:7149/api/CRUDFavoritePr/Delete", Guid.Parse(GetIdLogin), id);
            if (a == 0)
            {
                ViewData["XoaThatBai"] = "Xóa thất bại";
				return RedirectToAction("Index", "Product");
			}
            else
            {
                ViewData["XoaThanhCong"] = "Xóa tHành công";
				return RedirectToAction("Index", "Product");
			}

        }
        
    }
}
