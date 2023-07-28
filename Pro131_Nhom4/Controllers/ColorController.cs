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
        public async Task<IActionResult> GetAllColor()
        {
            var color = await _ColorService.GetAllColor();
            return Ok(color);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetColorById([FromRoute] Guid id)
        {
            var color = await _ColorService.GetColorById(id);
            return Ok(color);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetColorByName([FromRoute] string name)
        {
            var size = await _ColorService.GetColorByName(name);
            return Ok(size);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSize([FromForm] CreateColor color)
        {
            await _ColorService.CreateColor(color);
            return Ok(color);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSize([FromForm] UpdateColor color)
        {
            await _ColorService.UpdateColor(color);
            return Ok(color);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSize([FromRoute] Guid id)
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
