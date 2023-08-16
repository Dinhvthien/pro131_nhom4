using App_client.Services;
using App_Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.Data;
using System.Security.Claims;

namespace App_client.Controllers
{
    public class UserController : Controller
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
                    var result = await _services.GetAllById<User>($"https://localhost:7149/api/User/{userId}");
                    var getallrank = await _services.GetAll<Rank>("https://localhost:7149/api/Rank");
                    var ranks = getallrank.OrderByDescending(c => c.Point); // Sắp xếp rank từ cao đến thấp dựa trên điểm số
                    var rank = ranks.FirstOrDefault(c => c.Point <= result.Point);
                    ViewData["rank"] = rank.Name;
                    return View(result);
                }
                return RedirectToAction("Index", "Product");

            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
    }
}
