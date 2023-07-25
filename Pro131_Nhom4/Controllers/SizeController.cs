using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.IService;
using Pro131_Nhom4.Services;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/showsize")]
    [ApiController]
    public class SizeController : Controller
    {
        private readonly ISizeService _SizeService;

        public SizeController(ISizeService sizeService)
        {
            _SizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSize()
        {
            var size = await _SizeService.GetAllSize();
            return Ok(size);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetSizeById([FromRoute] Guid id)
        {
            var size = await _SizeService.GetSizeById(id);
            return Ok(size);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetSizeByName([FromRoute] string name)
        {
            var size = await _SizeService.GetSizeByName(name);
            return Ok(size);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSize([FromForm]CreateSize sizes)
        {
            await _SizeService.CreateSize(sizes);
            return Ok(sizes);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSize([FromForm]UpdateSize size)
        {
             await _SizeService.UpdateSize(size);
            return Ok(size);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSize([FromRoute] Guid id)
        {
            var size = await _SizeService.DeleteSize(id);
            if (size )
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
