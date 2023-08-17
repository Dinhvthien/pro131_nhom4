using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pro131_Nhom4.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Security.Principal;

namespace App_client.Controllers
{
	public class CheckourController : Controller
	{
		TServices _services = new TServices();
		HttpClient _httpClient;
		public INotyfService _notyfService { get; }
		public CheckourController(HttpClient httpClient, INotyfService notyfService)
		{
			_httpClient = httpClient;
			_notyfService = notyfService;

		}
		public async Task<IActionResult> PaymentOff()
		{
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			if (identity != null)
			{
				// productlist sai 

				var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
				if (userIdClaim != null)
				{
					var userName = userIdClaim.Value;
					// Sử dụng userId
					var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
					var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;

					List<CartDetails> getallcardt = await _services.GetAll<CartDetails>("https://localhost:7149/api/cartdt");
					var getcartbyid = getallcardt.Where(c => c.AccountID == userId);


					var product = await _services.GetAll<Product>("https://localhost:7149/api/showlist");
					double total = 0;
					var productCart = new List<CartProductViewModel>();

					foreach (var item in getcartbyid)
					{
						var productInfo = product.FirstOrDefault(p => p.Id == item.ProductID);
						if (productInfo != null && item.Quantity >= 0 && productInfo.Price >= 0)
						{
							var cartProduct = new CartProductViewModel
							{
								ProductName = productInfo.Name,
								Quantity = item.Quantity,
								TotalPrice = (decimal)productInfo.Price,
								image = productInfo.ImageUrl

							};

							productCart.Add(cartProduct); // Thêm sản phẩm vào danh sách
							total += item.Quantity * productInfo.Price;

						}
					}
					ViewData["ProductCart"] = productCart;
					ViewData["TotalAmount"] = total;
					return View();
				}
				else
				{
					return RedirectToAction("Login", "Login");
				}
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}
		}
		[HttpPost]
		public async Task<IActionResult> PaymentOff(Bill bill, string voucher, int price)
		{
			Guid idbill ;
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			if (identity != null)
			{

				var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
				if (userIdClaim != null)
				{
					var userName = userIdClaim.Value;
					// Sử dụng userId
					var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
					var userId = getallUser.FirstOrDefault(c => c.UserName == userName);
					if (voucher== "" || voucher == null)
					{

						bill.Id = idbill = Guid.NewGuid();
						bill.VoucherID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66af36");
						bill.PayMentID = Guid.Parse("fca021a9-4c60-4692-bdc4-f1ddb0cf55b1");
						bill.AccountID = userId.Id;
						bill.CreateDate= DateTime.Now;
						bill.StatusID = Guid.Parse("968e5ad7-7c80-4ee7-8421-b5ba48e931ca");
						if (bill.Address == "" || bill.Address == null)
						{
							_notyfService.Error("bạn cần phải nhập địa chỉ");
							return RedirectToAction("PaymentOff", "Checkour");
						}
						
						var createBill = await _services.CreateAll<Bill>("https://localhost:7149/api/bill", bill);
		
                        if (createBill = true)
						{


							userId.Point += 100;


						//////////////////////////	var edit = await _services.EditAll<User>("https://localhost:7149/api/User", userId);
							_notyfService.Success("Thanh toán thành công");
							List<CartDetails> getallcardt = await _services.GetAll<CartDetails>("https://localhost:7149/api/cartdt");
							var getcartbyid = getallcardt.Where(c => c.AccountID == userId.Id);
							List<BillDetails> billDetails = new List<BillDetails>();
							List<Product> productList = await _services.GetAll<Product>("https://localhost:7149/api/showlist");

							foreach (var item in getcartbyid)
							{
								var productInfo = productList.FirstOrDefault(c => c.Id == item.ProductID);

								var billdetai = new BillDetails
								{
									Id = Guid.NewGuid(),
									BillID = idbill,
									ProductID = item.ProductID,
									Quantity = item.Quantity,
									Prices = productInfo.Price
								};
							

								await _httpClient.PutAsJsonAsync<Product>($"https://localhost:7149/api/showlist/test/{item.ProductID}/{item.Quantity}",null);
								billDetails.Add(billdetai);
								await _services.DeleteAll<CartDetails>($"https://localhost:7149/api/cartdt/{item.Id}"); // delete item in car

							}
							foreach (var billItem in billDetails)
							{
								var createBilldetail = await _services.CreateAll<BillDetails>("https://localhost:7149/api/billdt", billItem);
							
							}
							return RedirectToAction("billUser", "Checkour");
						}
						else
						{
							return RedirectToAction("PaymentOff", "Checkour");
						}
					}///////////____________________________________________________?????????????
					else
					{
						// Sử dụng userId
						var getallVoucher = await _services.GetAll<Voucher>("https://localhost:7149/api/voucher");
						var getvoucherbyname = getallVoucher.FirstOrDefault(c => c.VoucherName == voucher);
						Guid getvoucherbynameid;

						double giavoucher;

						if (getvoucherbyname != null)
						{
							giavoucher = getvoucherbyname.PercenDiscount;
							getvoucherbynameid = getvoucherbyname.Id;
						}
						else
						{
							giavoucher = 0;
							_notyfService.Error("voucher của bạn không tồn tại ");
							return RedirectToAction("PaymentOff", "Checkour");
							


						}
						idbill = bill.Id = Guid.NewGuid();
						bill.VoucherID = getvoucherbynameid;
						bill.PayMentID = Guid.Parse("fca021a9-4c60-4692-bdc4-f1ddb0cf55b1");
						bill.AccountID = userId.Id;
						bill.StatusID = Guid.Parse("968e5ad7-7c80-4ee7-8421-b5ba48e931ca");
						bill.Price = price - giavoucher;
						bill.CreateDate = DateTime.Now;
						if (bill.Address == "" || bill.Address == null)
						{
							_notyfService.Error("bạn cần phải nhập địa chỉ");
							return RedirectToAction("PaymentOff", "Checkour");
						}
						List<CartDetails> getallcardt = await _services.GetAll<CartDetails>("https://localhost:7149/api/cartdt");
						var getcartbyid = getallcardt.Where(c => c.AccountID == userId.Id);

						List<BillDetails> billDetails = new List<BillDetails>();
						List<Product> productList = await _services.GetAll<Product>("https://localhost:7149/api/showlist");

						bool tatCaSoLuongHopLe = true; // Cờ để xác định nếu tất cả số lượng đều hợp lệ

						foreach (var item in getallcardt)
						{
							var thongTinSanPham = productList.FirstOrDefault(p => p.Id == item.ProductID);

							// Kiểm tra nếu số lượng hàng trong giỏ vượt quá số lượng sản phẩm có sẵn
							if (thongTinSanPham == null || item.Quantity > thongTinSanPham.AvailableQuantity)
							{
								tatCaSoLuongHopLe = false; // Đặt cờ thành false
								break; // Thoát khỏi vòng lặp, không cần kiểm tra tiếp
							}
						}
						var createBill = await _services.CreateAll<Bill>("https://localhost:7149/api/bill", bill);
                        userId.Point += 100;


             /////////////////////////          // var edit = await _services.EditAll<User>("https://localhost:7149/api/User", userId);
                        if (createBill == true)
						{
							_notyfService.Success("Thanh toán thành công");
							foreach (var item in getcartbyid)
							{
								var productInfo = productList.FirstOrDefault(p => p.Id == item.ProductID);
								var billdetai = new BillDetails
								{
									Id = Guid.NewGuid(),
									BillID = idbill,
									ProductID = item.ProductID,
									Quantity = item.Quantity,
									Prices = productInfo.Price
								};
								billDetails.Add(billdetai);



								await _httpClient.PutAsJsonAsync<Product>($"https://localhost:7149/api/showlist/test/{item.ProductID}/{item.Quantity}", null);
								await _services.DeleteAll<CartDetails>($"https://localhost:7149/api/cartdt/{item.Id}"); // delete item in car
							}
							foreach (var billItem in billDetails)
							{
								var createBilldetail = await _services.CreateAll<BillDetails>("https://localhost:7149/api/billdt", billItem);
							}
							return RedirectToAction("billUser", "Checkour");
						}
						else
						{
							_notyfService.Error("Thanh toán không thành công");
							return RedirectToAction("PaymentOff", "Checkour");
						}
					}
				}
				else
				{
					return RedirectToAction("Login", "Login");
				}
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}
		}






		public async Task<IActionResult> billUser()
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
					Guid userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;


					var allbill = await _services.GetAll<Bill>("https://localhost:7149/api/bill");
					var billbyUser = allbill.Where(c => c.AccountID == userId);
					return View(billbyUser);
				}
				else
				{
					return RedirectToAction("Login", "Login");
				}
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}

		}
		public async Task<IActionResult> billdetail(Guid id)
		{
			List<Product> productList = await _services.GetAll<Product>("https://localhost:7149/api/showlist");

			var getallbilldetail = await _services.GetAll<BillDetails>("https://localhost:7149/api/billdt");
			var getbildetail = getallbilldetail.FindAll(c => c.BillID == id);
			ViewData["billdetail"] = productList;
			return View(getbildetail);
		}

		public IActionResult Payments()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Payments(string pm)
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
					List<CartDetails> danhSachCart = await _services.GetAll<CartDetails>("https://localhost:7149/api/cartdt");
					var gioHangTheoID = danhSachCart.Where(c => c.AccountID == userId);

					List<BillDetails> danhSachChiTietHoaDon = new List<BillDetails>();
					List<Product> danhSachSanPham = await _services.GetAll<Product>("https://localhost:7149/api/showlist");

					bool tatCaSoLuongHopLe = true; 

					foreach (var item in gioHangTheoID)
					{
						var thongTinSanPham = danhSachSanPham.FirstOrDefault(p => p.Id == item.ProductID);

					
						if (thongTinSanPham == null || item.Quantity > thongTinSanPham.AvailableQuantity)
						{
							tatCaSoLuongHopLe = false; 
							break;
						}
					}

					if (tatCaSoLuongHopLe)
					{

						if (pm == "true")// offline
						{
							return RedirectToAction("PaymentOff", "Checkour");
						}
						if (pm == "false")//online
						{

							return RedirectToAction("PaymentOn", "Checkour");
						}
					}
					else
					{
						return RedirectToAction("Index", "Cart");
					}
				}
				return RedirectToAction("Index", "home");

			}
			else
			{
				return RedirectToAction("Index", "home");
			}
			return View();
		}

		public IActionResult Viewhttt()
		{
			return View();
		}



		public async Task<IActionResult> PaymentOn()
		{
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			if (identity != null)
			{
				// productlist sai 

				var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
				if (userIdClaim != null)
				{
					var userName = userIdClaim.Value;
					// Sử dụng userId
					var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
					var userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;

					List<CartDetails> getallcardt = await _services.GetAll<CartDetails>("https://localhost:7149/api/cartdt");
					var getcartbyid = getallcardt.Where(c => c.AccountID == userId);


					var product = await _services.GetAll<Product>("https://localhost:7149/api/showlist");
					double total = 0;
					var productCart = new List<CartProductViewModel>();

					foreach (var item in getcartbyid)
					{
						var productInfo = product.FirstOrDefault(p => p.Id == item.ProductID);
						if (productInfo != null && item.Quantity >= 0 && productInfo.Price >= 0)
						{
							var cartProduct = new CartProductViewModel
							{
								ProductName = productInfo.Name,
								Quantity = item.Quantity,
								TotalPrice = (decimal)productInfo.Price,
								image = productInfo.ImageUrl

							};

							productCart.Add(cartProduct); // Thêm sản phẩm vào danh sách
							total += item.Quantity * productInfo.Price;

						}
					}
					ViewData["ProductCart"] = productCart;
					ViewData["TotalAmount"] = total;
					return View();
				}
				else
				{
					return RedirectToAction("Login", "Login");
				}
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}
		}
		[HttpPost]
		public async Task<IActionResult> PaymentOn(PaymentInformationModel model, Bill bill, string voucher, int price)
		{
			Guid idbill;
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			if (identity != null)
			{

				var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
				if (userIdClaim != null)
				{
					if (voucher == "")
					{
						var userName = userIdClaim.Value;
						// Sử dụng userId
						var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
						Guid userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
					bill.VoucherID = Guid.Parse("155c571b-b199-4544-f43c-08db95d4ff4f");
						bill.PayMentID = Guid.Parse("d58f71eb-1444-47c5-8928-e0ae0b0d5991");
						bill.AccountID = userId;
						bill.StatusID = Guid.Parse("968e5ad7-7c80-4ee7-8421-b5ba48e931ca");
						var createBill = await _services.CreateAll<Bill>("https://localhost:7149/api/bill", bill);

						if (createBill == true)
						{
							List<CartDetails> getallcardt = await _services.GetAll<CartDetails>("https://localhost:7149/api/cartdt");
							var getcartbyid = getallcardt.Where(c => c.AccountID == userId);
							List<BillDetails> billDetails = new List<BillDetails>();
							List<Product> productList = await _services.GetAll<Product>("https://localhost:7149/api/showlist");
							foreach (var item in getcartbyid)
							{
								var productInfo = productList.FirstOrDefault(p => p.Id == item.ProductID);
								var billdetai = new BillDetails
								{
									Id = Guid.NewGuid(),
									BillID = idbill = Guid.NewGuid(),
									ProductID = item.ProductID,
									Quantity = item.Quantity,
									Prices = productInfo.Price
								};
								billDetails.Add(billdetai);
								if (item.ProductID != null)
								{
									productInfo.AvailableQuantity -= item.Quantity;
									_services.EditAll($"https://localhost:7149/api/showlist/{item.ProductID}", productInfo);
								}
								await _services.DeleteAll<CartDetails>($"https://localhost:7149/api/cartdt/{item.Id}"); // delete item in car
							}
							foreach (var billItem in billDetails)
							{
								var createBilldetail = await _services.CreateAll<BillDetails>("https://localhost:7149/api/billdt", billItem);
							}
							return RedirectToAction("billUser", "Checkour");
						}
						else
						{
							return RedirectToAction("PaymentOff", "Checkour");
						}
					}///////////____________________________________________________?????????????
					else
					{
						var userName = userIdClaim.Value;
						// Sử dụng userId
						var getallVoucher = await _services.GetAll<Voucher>("https://localhost:7149/api/voucher");
						var getvoucherbyname = getallVoucher.FirstOrDefault(c => c.VoucherName == voucher);
						Guid getvoucherbynameid;

						double giavoucher;

						if (getvoucherbyname != null)
						{
							giavoucher = getvoucherbyname.PercenDiscount;
							getvoucherbynameid = getvoucherbyname.Id;
						}
						else
						{
							giavoucher = 0;
							getvoucherbynameid = Guid.Parse("155c571b-b199-4544-f43c-08db95d4ff4f");
						}

						var getallUser = await _services.GetAll<User>("https://localhost:7149/api/User");
						Guid userId = getallUser.FirstOrDefault(c => c.UserName == userName).Id;
						idbill = bill.Id = Guid.NewGuid();
						bill.VoucherID = getvoucherbynameid;
						bill.PayMentID = Guid.Parse("d58f71eb-1444-47c5-8928-e0ae0b0d5991");
						bill.AccountID = userId;
						bill.StatusID = Guid.Parse("968e5ad7-7c80-4ee7-8421-b5ba48e931ca");
					model.Amount =	bill.Price = price - giavoucher;
						bill.CreateDate = DateTime.Now;
						if (bill.Address == "" || bill.Address == null)
						{
							return RedirectToAction("PaymentOff", "Checkour");
						}
						
						List<CartDetails> getallcardt = await _services.GetAll<CartDetails>("https://localhost:7149/api/cartdt");
						var getcartbyid = getallcardt.Where(c => c.AccountID == userId);

						List<BillDetails> billDetails = new List<BillDetails>();
						List<Product> productList = await _services.GetAll<Product>("https://localhost:7149/api/showlist");

						bool tatCaSoLuongHopLe = true; // Cờ để xác định nếu tất cả số lượng đều hợp lệ

						foreach (var item in getallcardt)
						{
							var thongTinSanPham = productList.FirstOrDefault(p => p.Id == item.ProductID);

							// Kiểm tra nếu số lượng hàng trong giỏ vượt quá số lượng sản phẩm có sẵn
							if (thongTinSanPham == null || item.Quantity > thongTinSanPham.AvailableQuantity)
							{
								tatCaSoLuongHopLe = false; // Đặt cờ thành false
								break; // Thoát khỏi vòng lặp, không cần kiểm tra tiếp
							}
						}
						var createBill = await _services.CreateAll<Bill>("https://localhost:7149/api/bill", bill);
						if (createBill == true)
						{

							foreach (var item in getcartbyid)
							{
								var productInfo = productList.FirstOrDefault(p => p.Id == item.ProductID);
								var billdetai = new BillDetails
								{
									Id = Guid.NewGuid(),
									BillID = idbill,
									ProductID = item.ProductID,
									Quantity = item.Quantity,
									Prices = productInfo.Price
								};
								billDetails.Add(billdetai);
								if (item.ProductID != null)
								{
									productInfo.AvailableQuantity -= item.Quantity;
									_services.EditAll($"https://localhost:7149/api/showlist/{item.ProductID}", productInfo);
								}
								await _services.DeleteAll<CartDetails>($"https://localhost:7149/api/cartdt/{item.Id}"); // delete item in car
							}
							foreach (var billItem in billDetails)
							{
								var createBilldetail = await _services.CreateAll<BillDetails>("https://localhost:7149/api/billdt", billItem);
							}
							//var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
							//return Redirect(url);
							return RedirectToAction("billUser", "Checkour");
						}
						else
						{
							return RedirectToAction("PaymentOn", "Checkour");
						}
					}
				}
				else
				{
					return RedirectToAction("Login", "Login");
				}
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}
		}


		//public IActionResult PaymentCallback()
		//{
		//	var response = _vnPayService.PaymentExecute(Request.Query);
		//	if (response.VnPayResponseCode == "00")
		//	{
		//		return RedirectToAction("billUser", "Checkour"); // thanh toán thành công
		//	}
		//	else
		//	{

		//		return RedirectToAction("PaymentOn", "Checkour");// thanh toán false
		//	}
		//}

	}
}
