using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pro131_Nhom4.Data;
using System.Security.Claims;

namespace App_client.Controllers
{
    public class ProductController : Controller
    {
        TServices _services = new TServices();
        public async Task<IActionResult> Index(string name)
        {
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim == null)
            {
                var product = await _services.GetAll<ProductView>("https://localhost:7149/api/showlist");
                var p = product.GroupBy(p => new { p.Name }).Select(g => g.First()).ToList();
                return View(p);
            }
            else
            {
                var userName = userIdClaim.Value;
                var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
                var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
                var result = await _services.GetAll<ViewFavoriteProduct>("https://localhost:7149/api/CRUDFavoritePr/GetAll");
                var productfavorite = result.FindAll(c => c.AccountID == userId);
                var product = await _services.GetAll<ProductView>("https://localhost:7149/api/showlist");
                ViewData["yeuthich"] = productfavorite;
                var p = product.GroupBy(p => new { p.Name }).Select(g => g.First()).ToList();
                return View(p);
            }
        }
        public async Task<IActionResult> Tang()
        {
            var product = await _services.GetAll<ProductView>("https://localhost:7149/api/showlist");
            var p = product.OrderBy(p => p.Price ).Select(p =>p.Price).ToList();
            return View("Index", p);
        }
        public async Task<IActionResult> Giam()
        {
            var product = await _services.GetAll<ProductView>("https://localhost:7149/api/showlist");
            var p = product.OrderByDescending(p => p.Price).Select(p => p.Price).ToList();
            return View("Index", p);
        }
        public async Task<IActionResult> Search(string search)
		{
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                var product = await _services.GetAll<ProductView>("https://localhost:7149/api/showlist");
                var p = product.GroupBy(p => new { p.Name }).Select(g => g.First()).ToList();
				return View("Index",p);
			}
            else
            {
                var userName = userIdClaim.Value;
                var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
                var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
                var result = await _services.GetAll<ViewFavoriteProduct>("https://localhost:7149/api/CRUDFavoritePr/GetAll");
                var productfavorite = result.FindAll(c => c.AccountID == userId);
                var product = await _services.GetAll<ProductView>($"https://localhost:7149/api/showlist/{search}");
                ViewData["yeuthich"] = productfavorite;
                var p = product.GroupBy(p => new { p.Name }).Select(g => g.First()).ToList();
                return View("Index", p);
            }
        }
        public async Task<IActionResult> Details(Guid id)
        {
          // check tt user

            var result = await _services.GetAllById<Product>($"https://localhost:7149/api/showlist/{id}");
            var name = await _services.GetAll<ProductView>($"https://localhost:7149/api/showlist/{result.Name}");
            List<Sizes> size = new List<Sizes>();
            foreach (var item in name)
            {

                bool b = false;
                foreach (var items in size)
                {
                    if (items.Id == item.SizeID)
                    {
                        b = true;
                        break;
                    }
                }
                if (!b)
                    size.Add(item.Size);
            }
            //ViewData["colors"] = color;
            ViewData["sizes"] = size;

            return View(result);
        }
    }
}
