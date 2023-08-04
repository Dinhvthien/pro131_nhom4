using App_Shared.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Pro131_Nhom4.Data;
using App_client.Services;
using Microsoft.AspNetCore.Http;

namespace App_client.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly TServices _services;
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _services = new TServices();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            // Convert registerUser to JSON
            var loginUserJSON = JsonConvert.SerializeObject(loginUser);

            // Convert to string content
            var stringContent = new StringContent(loginUserJSON, Encoding.UTF8, "application/json");

            // Send request POST to register API
            var response = await _httpClient.PostAsync($"https://localhost:7149/api/Login", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value));
                 identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value));


                var username_md = jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
                var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
                var userId = getallUser.FirstOrDefault(c => c.UserName == username_md).Id;
                HttpContext.Session.SetString("IdLogin", userId.ToString());

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);
                var role = identity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
                //var check = User.Identity.IsAuthenticated;
               // string userRole = jwt.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value; 
                if (role == "Admin")
                {
					return RedirectToAction("Index", "Admin", new { area = "Admin" });

				}
				else if(role == "User")
                {
                    return RedirectToAction("Index", "Product");
                }else
                {
                     return RedirectToAction("Register", "Register");
                }               
            }
            else
            {
                ViewBag.Message = await response.Content.ReadAsStringAsync();
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
