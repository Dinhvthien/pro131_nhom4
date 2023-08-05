using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pro131_Nhom4.Data;
using System.Text;

namespace App_client.Controllers
{
	public class RegisterController : Controller
	{
		TServices _services = new TServices();
		private readonly HttpClient _httpClient;
		public RegisterController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]

		public async Task<IActionResult> Register(RegisterUser registerUser, string role)
		{
			registerUser.id = Guid.NewGuid();
			// Convert registerUser to JSON
			var registerUserJSON = JsonConvert.SerializeObject(registerUser);
			// Convert to string content
			var stringContent = new StringContent(registerUserJSON, Encoding.UTF8, "application/json");
			// Add role to queryString
			role = "User";
			var queryString = $"?role={role}";
			var response = await _httpClient.PostAsync($"https://localhost:7149/api/Register{queryString}", stringContent);
			ViewBag.Message = await response.Content.ReadAsStringAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}
