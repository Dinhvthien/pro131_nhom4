using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Pro131_Nhom4.Data;
using System.Net.Http;
using System.Security.Claims;

namespace App_client.Controllers
{
	public class CartController : Controller
	{
		TServices _services = new TServices();
		private readonly HttpClient _httpClient;
		public CartController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<ActionResult> AddtoCart([FromForm] string Namespp, Guid idsize, Guid idcolor, int slsp)
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

					var getallCart = await _services.GetAllById<Cart>($"https://localhost:7149/api/cart/{userId}");
					var product = await _services.GetAll<Product>("https://localhost:7149/api/showlist");
					var getidsp = product.FirstOrDefault(c => c.Name == Namespp && c.SizeID == idsize && c.ColorID == idcolor);
					if (getidsp.AvailableQuantity >= slsp)
					{
						if (getallCart == null)
						{
							if(slsp<=0)
							{
								return RedirectToAction("Details", "Product", new { id = getidsp.Id });
							}
							Cart cart = new Cart();
							cart.UserID = userId;
							cart.Description = "Nguoi dung dep trai";
							var result = await _services.CreateAll<Cart>("https://localhost:7149/api/cart", cart);
							if (result)
							{

								if (getidsp.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
								{
									return View();
								}
								else
								{
									CartDetails cartDetails = new CartDetails();
									cartDetails.Id = Guid.NewGuid();
									cartDetails.AccountID = userId;
									cartDetails.ProductID = getidsp.Id;
									cartDetails.Quantity = slsp;
									await _services.CreateAll<CartDetails>("https://localhost:7149/api/cartdt", cartDetails);


									return RedirectToAction("Index", "Cart");
								}

							}
						}
						else
						{
	

							if (getidsp.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
							{
								return RedirectToAction("Index", "Cart");
							}
							if (slsp <= 0)
							{
								return RedirectToAction("Details", "Product", new { id = getidsp.Id });
							}
							CartDetails cartDetails = new CartDetails
							{
								Id = Guid.NewGuid(),
								AccountID = userId,
								ProductID = getidsp.Id,
								Quantity = slsp
							};
							var cartResponse = await _httpClient.PostAsJsonAsync("https://localhost:7149/api/cartdt", cartDetails);
							if (cartResponse.IsSuccessStatusCode)
							{
								return RedirectToAction("Index", "Cart");
							}

						}
					}
					else
					{
						//hine thi thong bao so luong sp khong du 
						return RedirectToAction("Details", "Product", new { id = getidsp.Id });
					}				
				}
				return RedirectToAction("Login", "Login");
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}
		}
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
					var result = await _services.GetAll<CartDetails>("https://localhost:7149/api/cartdt");
					var getallcartbyuser = result.FindAll(c => c.AccountID == userId).ToList();
					var productIds = getallcartbyuser.Select(c => c.ProductID).ToList();
					var productList = new List<Product>();

					foreach (var productId in productIds)
					{
						var product = await _services.GetAllById<Product>($"https://localhost:7149/api/showlist/{productId}");
						productList.Add(product);
					}
					var countsp = getallcartbyuser.Count();

					ViewData["productList"] = productList;
					HttpContext.Session.SetInt32("countspcart", countsp);
					return View(getallcartbyuser);
				}
				return RedirectToAction("Index", "home");

			}
			else
			{
				return RedirectToAction("Index", "home");
			}
		 

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

		[HttpGet]
		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _services.DeleteAll<CartDetails>($"https://localhost:7149/api/cartdt/{id}");
			return RedirectToAction("Index");
		}


	}
}
