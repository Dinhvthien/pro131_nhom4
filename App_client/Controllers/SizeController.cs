using App_client.Services;
using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net.Http;
using System.Net.WebSockets;

namespace App_client.Controllers
{
    public class SizeController : Controller
    {
		private readonly ILogger<SizeController> _logger;
		private readonly TServices _services;

		public SizeController(ILogger<SizeController> logger)
		{
			_services = new TServices();
			_logger = logger;
		}
		public async Task<IActionResult> Index()
        {
            var result = await _services.GetAll<ViewSize>("https://localhost:7149/api/Size/Get-All");
            
            return View(result);
        }
        public IActionResult Create()
		{
			return View();
		}
		[HttpPost]

		public async Task<IActionResult> Creating(CreateSize createSize)
		{
			var result = await _services.CreateAll<CreateSize>("https://localhost:7149/api/Size/CreateSize", createSize);
			if (result)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
            var result = await _services.GetById_DungBM<ViewSize>("https://localhost:7149/api/Size/GetById/",id);
            return View(result);
		}

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var result = await _services.GetById_DungBM<ViewSize>("https://localhost:7149/api/Size/GetById/", id);
            UpdateSize updateSize = new UpdateSize();
			updateSize.Id = id;
			updateSize.Status = result.Status;
			updateSize.Name = result.Name;
            return View(updateSize);
        }
		[HttpPost]
		public async Task<IActionResult> Editing(UpdateSize model)
		{
			await _services.Update_DungBM<UpdateSize>("https://localhost:7149/api/Size/Update/", model, model.Id);
			return RedirectToAction("Index");
		}
        public async Task<IActionResult> Delete(Guid id)
        {
			var a = await _services.Delete_DungBM<ViewSize>("https://localhost:7149/api/Size/GetById/", "https://localhost:7149/api/Size/Delete/", id);
			if (a == 0)
			{
				ViewData["XoaThatBai"] = "Xóa thất bại";
				return View("Index");
            }
			else
			{
                ViewData["XoaThanhCong"] = "Xóa tHành công";
                return RedirectToAction("Index");
            }
           
        }
    }
}

