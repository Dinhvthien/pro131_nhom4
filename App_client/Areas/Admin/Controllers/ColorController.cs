using App_client.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.WebSockets;
using App_Shared.ViewModels;
using App_Shared.Model;

namespace App_client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly ILogger<ColorController> _logger;
        private readonly TServices _services;

        public ColorController(ILogger<ColorController> logger)
        {
            _services = new TServices();
            _logger = logger;
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAll<ViewColor>("https://localhost:7149/api/Color/Get-All");
            return View(result);
        }

        public IActionResult CreateColor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateColor(CreateColor createColor)
        {
            var result = await _services.CreateAll("https://localhost:7149/api/Color/CreateColor", createColor);
            //if (result)
            //{
            //	return RedirectToAction("GetAll");
            //}
            //return View(result);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _services.GetById_DungBM<ViewColor>("https://localhost:7149/api/Color/GetColorById/", id);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var result = await _services.GetById_DungBM<ViewColor>("https://localhost:7149/api/Color/GetById/", id);
            UpdateColor updateColor = new UpdateColor();
            updateColor.Id = id;
            updateColor.Name = result.Name;
            updateColor.Status = result.Status;
            return View(updateColor);
        }
        [HttpPost]
        public async Task<IActionResult> Editing(UpdateColor updateColor)
        {
            await _services.Update_DungBM("https://localhost:7149/api/Color/Update/", updateColor, updateColor.Id);
            return RedirectToAction("GetAll");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var a = await _services.Delete_DungBM<ViewColor>("https://localhost:7149/api/Color/GetById/", "https://localhost:7149/api/Color/Delete/", id);

            if (a == 0)
            {
                ViewData["XoaThatBai"] = "Xóa thất bại";
                return View("GetAll");
            }
            else
            {
                ViewData["XoaThanhCong"] = "Xóa tHành công";
                return RedirectToAction("GetAll");
            }

        }

    }
}
