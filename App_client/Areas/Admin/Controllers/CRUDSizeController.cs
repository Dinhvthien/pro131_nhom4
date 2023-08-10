using App_client.Controllers;
using App_client.Services;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App_client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CRUDSizeController : Controller
	{
        TServices _services = new TServices();
        public async Task<IActionResult> AllSize()
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
				return RedirectToAction("AllSize");
			}
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var result = await _services.GetById_DungBM<ViewSize>("https://localhost:7149/api/Size/GetById/", id);
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
			return RedirectToAction("AllSize");
		}
		public async Task<IActionResult> Delete(Guid id)
		{
			var a = await _services.Delete_DungBM<ViewSize>("https://localhost:7149/api/Size/GetById/", "https://localhost:7149/api/Size/Delete/", id);
			if (a == 0)
			{
				ViewData["XoaThatBai"] = "Xóa thất bại";
				return View("AllSize");
			}
			else
			{
				ViewData["XoaThanhCong"] = "Xóa tHành công";
				return RedirectToAction("AllSize");
			}

		}
	}
}
