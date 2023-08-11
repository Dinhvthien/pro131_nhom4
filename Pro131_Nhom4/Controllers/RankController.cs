using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankController : ControllerBase
    {
        private readonly IRankService _rankService;
        public RankController(IRankService rankService)
        {
            _rankService = rankService;
        }

        [HttpGet]
        public async Task<IActionResult> ShowListRank()
        {
            var rank = await _rankService.GetAllRank();
            return Ok(rank);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetRankById( Guid id)
        {
            var product = await _rankService.GetRankById (id);
            return Ok(product);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductByName( string name)
        {
            var product = await _rankService.GetRankByName(name);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRank(CreateRank createRank)
        {
            await _rankService.CreateRank(createRank);
            return Ok(createRank);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRank(UpdateRank updateRank)
        {
            await _rankService.UpdateRank(updateRank);
            return Ok(updateRank);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRank( Guid id)
        {
            await _rankService.DeleteRank(id);
            return Ok();
        }
    }
}
