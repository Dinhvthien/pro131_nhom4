using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App_client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        TServices _services = new TServices();
        public async Task<IActionResult> Index()
        {
            var product = await _services.GetAll<ProductView>("https://localhost:7149/api/showlist");
            
            return View(product);
        }
        public async Task<IActionResult> Search(string search)
        {

            return View("Index", await _services.GetAll<ProductView>($"https://localhost:7149/api/showlist/{search}"));
        }
        public async Task<IActionResult> Details(Guid id)
        {

            var result = await _services.GetAllById<ProductView>($"https://localhost:7149/api/showlist/{id}");
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

            //var allColor = await _services.GetAll<Colors>("https://localhost:7149/api/Color");
            //List<Colors> color = new List<Colors>();
            //foreach (var item in allColor)
            //{
            //    color.Add(item);
            //}
            //ViewData["color"] = color;
            var allSize = await _services.GetAll<Sizes>("https://localhost:7149/api/Size/Get-All");
            List<Sizes> size = new List<Sizes>();
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
            //product.Status = 0;
            //product.Likes = 0;
            product.ColorID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66aee6");
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

            await _services.CreateAll("https://localhost:7149/api/showlist", product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)//Mở form
        {
            var allSize = await _services.GetAll<Sizes>("https://localhost:7149/api/Size/Get-All");
            List<Sizes> size = new List<Sizes>();
            foreach (var item in allSize)
            {
                size.Add(item);
            }
            ViewData["size"] = size;
            var product = await _services.GetAllById<ProductView>($"https://localhost:7149/api/showlist/{id}");
            return View(product);
        }
        public async Task<IActionResult> Edit(ProductView product, IFormFile imageFile)
        {
            product.ColorID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66aee6");
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
            await _services.EditAll($"https://localhost:7149/api/showlist/{product.Id}", product);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteProduct(Guid id)
        {

            await _services.DeleteAll<Product>($"https://localhost:7149/api/showlist/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Like(Guid id)
        {
            var user = SessionServices.GetAccountFromSession(HttpContext.Session, "User");
            HttpClient httpClient = new HttpClient(); // tạo ra để callApi
            var request = new HttpRequestMessage(HttpMethod.Options, $"https://localhost:7149/api/favoriteProducts/{user.Id}/{id}");
            await httpClient.SendAsync(request);
            return RedirectToAction("Index", "FavoriteProducts");
        }
    }
}
