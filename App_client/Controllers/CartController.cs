using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.Data;
using System.Security.Claims;

namespace App_client.Controllers
{
    public class CartController : Controller
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
                    var result = await _services.GetAll<CartView>($"https://localhost:7149/cart/{userId}");
                    return View(result);
                }
                return RedirectToAction("Index", "Admin", new { area = "Admin" });

            }
            else
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
         ;

        }
        public async Task<IActionResult> Search(string search)
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
            return View(await _services.GetAll<BillView>($"https://localhost:7256/cart{search}"));
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
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
            return View(await _services.GetAllById<BillView>($"https://localhost:7256/cart/{id}"));
        }
        [HttpPost]
        
        public async Task<IActionResult> Create(Cart cart)
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
            await _services.CreateAll("https://localhost:7149/cart", cart);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [HttpPost]
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
            await _services.DeleteAll<Cart>($"https://localhost:7149/cart/{id}");
            return RedirectToAction("Index");
        }
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
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
            var bill = await _services.GetAllById<Cart>($"https://localhost:7149/cart/{id}");
            return View(bill);
        }
        public async Task<IActionResult> Edit(Cart cart)
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
            await _services.EditAll($"https://localhost:7149/api/{cart.UserID}", cart);
            return RedirectToAction("Index");
        }
    }
}
