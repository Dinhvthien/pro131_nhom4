using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App_client.Controllers
{
    public class ProductController : Controller
    {
        TServices _services = new TServices();
        public async Task<IActionResult> Index()
        {
            /*  var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
              if (user.Status != 404)
              {
                  ViewBag.RoleId = user.RoleId;
                  if (user.RoleId == Guid.Parse("9d76eb12-8c3c-4dcf-a389-4a807ecf0a31"))
                  {
                      return View(await _services.GetAll<ProductView>("https://localhost:7256/api/showlist"));
                  }
              }*/
            var product = await _services.GetAll<ProductView>("https://localhost:7149/api/showlist");
            var p = product.GroupBy(p => new { p.Name, p.ColorID }).Select(g => g.First()).ToList();
            return View(p);
        }
        public async Task<IActionResult> Search(string search)
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
    
            return View("Index", await _services.GetAll<ProductView>($"https://localhost:7149/api/showlist/{search}"));
        }
        public async Task<IActionResult> Details(Guid id)
        {
          // check tt user

            var result = await _services.GetAllById<Product>($"https://localhost:7149/api/showlist/{id}");
            var name = await _services.GetAll<ProductView>($"https://localhost:7149/api/showlist/{result.Name}");
            List<Colors> color = new List<Colors>();
            List<Sizes> size = new List<Sizes>();
            foreach (var item in name)
            {
                bool a = false;
                foreach (var items in color)
                {
                    if (items.Id == item.ColorID)
                    {
                        a = true;
                        break;
                    }
                }
                if (!a)
                    color.Add(item.Color);
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
            ViewData["colors"] = color;
            ViewData["sizes"] = size;
            return View(result);
        }
    }
}
