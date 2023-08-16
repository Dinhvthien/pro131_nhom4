using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.Data;
using System.Security.Claims;

namespace App_client.Controllers
{
    public class CartDetailsController : Controller
    {
        TServices _services = new TServices();
        public async Task<IActionResult> Index()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    // Sử dụng userId
                    var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
                    var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
                    var result = await _services.GetAll<CartDetailsView>($"https://localhost:7149/cart/{userId}");
                    return View(result);
                }
                return RedirectToAction("Index", "Product");

            }
            else
            {
               return RedirectToAction("Index", "Product");
            }
           ;

        }

       /* [Route("[action]/{id}")]
        public async Task<IActionResult> Create(Guid id)
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
                HttpClient httpClient = new HttpClient(); // tạo ra để callApi
                await httpClient.PostAsync($"https://localhost:7197/api/cartdetails/{user.Id}/{id}", null);
                return RedirectToAction("Index", "CartDetails");
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddProductToCartView product)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    // Sử dụng userId
                    var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
                    var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
                    HttpClient httpClient = new HttpClient(); // tạo ra để callApi
                    var result = await _services.GetAllById<Product>($"https://localhost:7149/api/showlist/{product.Name}/{product.ColorID}/{product.SizeID}");
                    await httpClient.PostAsync($"https://localhost:7256/cartdt/{userId}/{result.Id}", null);

                    //return View(result);
                    return RedirectToAction("Index", "CartDetails");
                }
                return RedirectToAction("Index", "Admin", new { area = "Admin" });

            }
            else
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }

        }*/
       /* [HttpGet]
        [HttpPost]
        [Route("[action]/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    // Sử dụng userId
                    var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
                    var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
                    var result = await _services.GetAllById<User>($"https://localhost:7149/api/User/{userId}");
                    return View(result);
                }
                return RedirectToAction("Index", "Admin", new { area = "Admin" });

            }
            else
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            await _services.DeleteAll<CartDetails>($"https://localhost:7149/cartdt/{id}");
            return RedirectToAction("Index");
        }*/

       /* [Route("[action]/{id:Guid}")]
        public async Task<IActionResult> Increase(Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    // Sử dụng userId
                    var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
                    var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
                    var result = await _services.GetAllById<User>($"https://localhost:7149/api/User/{userId}");
                    return View(result);
                }
                return RedirectToAction("Index", "Admin", new { area = "Admin" });

            }
            else
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            await _services.EditAll<CartDetails>($"https://localhost:7149/cartdt/Increase/{id}", null);
            return RedirectToAction("Index");
        }

        [Route("[action]/{id:Guid}")]
        public async Task<IActionResult> Reduce(Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    // Sử dụng userId
                    var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
                    var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
                    var result = await _services.GetAllById<User>($"https://localhost:7149/api/User/{userId}");
                    return View(result);
                }
                return RedirectToAction("Index", "Admin", new { area = "Admin" });

            }
            else
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            await _services.EditAll<CartDetails>($"https://localhost:7149/cartdt/Reduce/{id}", null);
            return RedirectToAction("Index");
        }*/
    }
}
