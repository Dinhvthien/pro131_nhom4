using App_Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _ColorService;

        public ColorController(IColorService colorService)
        {
            _ColorService = colorService;
        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllColor()
        {
            var color = await _ColorService.GetAllColor();
            return Ok(color);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetColorById( Guid id)
        {
            var color = await _ColorService.GetColorById(id);
            return Ok(color);
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public async Task<IActionResult> GetColorByName( string name)
        {
            var size = await _ColorService.GetColorByName(name);
            return Ok(size);
        }

        [HttpPost]
        [Route("CreateColor")]
        public async Task<IActionResult> CreateSize( CreateColor color)
        {
            await _ColorService.CreateColor(color);
            return Ok(color);
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateSize(UpdateColor color)
        {
            await _ColorService.UpdateColor(color);
            return Ok(color);
        }
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteSize( Guid id)
        {
            var size = await _ColorService.DeleteColor(id);
            if (size)
            {
                return Ok("Xóa thành công");
            }
            else
            {
                return Ok("Xóa không thành công");
            }
        }
    }
}
