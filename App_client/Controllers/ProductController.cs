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
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
            }
            return View("Index", await _services.GetAll<ProductView>($"https://localhost:7149/api/showlist/{search}"));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
            }
            var result = await _services.GetAllById<Product>($"https://localhost:7256/api/showlist/{id}");
            var name = await _services.GetAll<ProductView>($"https://localhost:7256/api/showlist/{result.Name}");
            List<Color> color = new List<Color>();
            List<Size> size = new List<Size>();
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
                /*// select sort
                for (int i = 0; i < color.Count - 1; i++)
                {
                    for (int j = i + 1; j < color.Count; j++)
                    {
                        if (Convert.ToUInt32(color[i].Name) < Convert.ToUInt32(color[j].Name))
                        {
                            Color c = color[i];
                            color[i] = color[j];
                            color[j] = c;
                        }
                    }
                }*/
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
        public async Task<IActionResult> Create()//Mở form
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
            }
            var allColor = await _services.GetAll<Color>("https://localhost:7256/api/color");
            List<Color> color = new List<Color>();
            foreach (var item in allColor)
            {
                color.Add(item);
            }
            ViewData["color"] = color;
            var allSize = await _services.GetAll<Size>("https://localhost:7256/api/size");
            List<Size> size = new List<Size>();
            foreach (var item in allSize)
            {
                size.Add(item);
            }
            ViewData["size"] = size;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
            }
            product.Status = 0;
            product.Likes = 0;
            if (imageFile != null && imageFile.Length > 0)//Kiểm tra đường dẫn phù hợp
            {
                //thực hiện sao chép ảnh đó vào wwwroot
                //Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);//abc/wwwroot/Images/xxx.png
                var stream = new FileStream(path, FileMode.OpenOrCreate);//Tạo 1 filestream để tạo mới
                await imageFile.CopyToAsync(stream);//copy ảnh vừa được chọn vào đúng cái stream đó
                                                    //gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính ImageUrl)
                product.ImageUrl = imageFile.FileName;
            }
            await _services.CreateAll("https://localhost:7256/api/showlist", product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)//Mở form
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
            }
            var allColor = await _services.GetAll<Color>("https://localhost:7256/api/color");
            List<Color> color = new List<Color>();
            foreach (var item in allColor)
            {
                color.Add(item);
            }
            ViewData["color"] = color;
            var allSize = await _services.GetAll<Size>("https://localhost:7256/api/size");
            List<Size> size = new List<Size>();
            foreach (var item in allSize)
            {
                size.Add(item);
            }
            ViewData["size"] = size;
            var product = await _services.GetAllById<ProductView>($"https://localhost:7256/api/showlist/{id}");
            return View(product);
        }
        public async Task<IActionResult> Edit(ProductView product, IFormFile imageFile)
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
            }
            if (imageFile != null && imageFile.Length > 0)//Kiểm tra đường dẫn phù hợp
            {
                //thực hiện sao chép ảnh đó vào wwwroot
                //Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);//abc/wwwroot/Images/xxx.png
                var stream = new FileStream(path, FileMode.OpenOrCreate);//Tạo 1 filestream để tạo mới
                await imageFile.CopyToAsync(stream);//copy ảnh vừa được chọn vào đúng cái stream đó
                                                    //gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính ImageUrl)
                product.ImageUrl = imageFile.FileName;
            }
            await _services.EditAll($"https://localhost:7256/api/showlist/{product.Id}", product);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
            }
            await _services.DeleteAll<Product>($"https://localhost:7256/api/showlist/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Like(Guid id)
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            if (user.Status != 404)
            {
                ViewBag.RoleId = user.RoleId;
            }
            HttpClient httpClient = new HttpClient(); // tạo ra để callApi
            var request = new HttpRequestMessage(HttpMethod.Options, $"https://localhost:7256/api/favoriteProducts/{user.Id}/{id}");
            await httpClient.SendAsync(request);
            return RedirectToAction("Index", "FavoriteProducts");
        }
    }
}
