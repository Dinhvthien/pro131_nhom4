using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pro131_Nhom4.IService;
using App_Shared.Model;
using App_Shared.ViewModels;
using Pro131_Nhom4.Services;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/favoriteproducts")]
    [ApiController]
    public class CRUDFavoritePrController : Controller
    {
        private readonly ICRUDFavoriteProductService _ICRUDFavoriteProductService;

        public CRUDFavoritePrController(ICRUDFavoriteProductService ICRUDFavoriteProductService)
        {
            _ICRUDFavoriteProductService = ICRUDFavoriteProductService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavoritePR([FromForm] CreateFavoriteProducts createfavoriteproducts)
        {
            await _ICRUDFavoriteProductService.CreateFavoriteProduct(createfavoriteproducts);
            return Ok(createfavoriteproducts);
        }
        [HttpDelete("{idacc},{idproduct}")]
        public async Task<IActionResult> DeleteFavoriteProduct([FromRoute] Guid idacc , Guid idproduct)
        {
            var deleteFavoritePR = await _ICRUDFavoriteProductService.DeleteFavoriteProduct(idacc,idproduct);
            if (deleteFavoritePR)
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
