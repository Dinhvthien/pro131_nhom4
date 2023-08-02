using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.Data;
using System.Security.Claims;

namespace App_client.Areas.Admin.Controllers
{
	[Area("Admin")]

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
					var userId = getallUser.FirstOrDefault(c=>c.UserName == userName).Id;
					var result = await _services.GetAllById<User>($"https://localhost:7149/api/User/{userId}");
					return View(result);
				}
				return RedirectToAction("Index", "Admin", new { area = "Admin" });

			}
			else
			{
				return RedirectToAction("Index", "Admin", new { area = "Admin" });
			}
		}
	}
}
