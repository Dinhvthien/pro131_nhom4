using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.IService;
using Pro131_Nhom4.Services;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : Controller
    {
        private readonly ISizeService _SizeService;

        public SizeController(ISizeService sizeService)
        {
            _SizeService = sizeService;
        }

        [HttpGet]
		[Route("Get-All")]
		public async Task<IActionResult> GetAllSize()
        {
            var size = await _SizeService.GetAllSize();
            return Ok(size);
        }

        //[HttpGet("{id:Guid}")]
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetSizeById(Guid id)
        {
            var size = await _SizeService.GetSizeById(id);
            return Ok(size);
        }

        //[HttpGet("{name}")]
        [HttpPost]
        [Route("GetByName/{name}")]
        public async Task<IActionResult> GetSizeByName(string name)
        {
            var size = await _SizeService.GetSizeByName(name);
            return Ok(size);
        }

        [HttpPost]
		[Route("CreateSize")]
		public async Task<IActionResult> CreateSize(CreateSize sizes)
        {
            await _SizeService.CreateSize(sizes);
            return Ok(sizes);
        }
        //[HttpPut]
        [HttpPost]
        [Route("Update/{id}")]
		public async Task<IActionResult> UpdateSize(UpdateSize size)
        {
             await _SizeService.UpdateSize(size);
            return Ok(size);
        }
        //[HttpDelete("{id}")]
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteSize(Guid id)
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
