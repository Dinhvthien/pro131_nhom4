using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pro131_Nhom4.IService;
using App_Shared.Model;
using App_Shared.ViewModels;
using Pro131_Nhom4.Services;
using System.Net.WebSockets;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDFavoritePrController : Controller
    {
        private readonly ICRUDFavoriteProductService _ICRUDFavoriteProductService;

        public CRUDFavoritePrController(ICRUDFavoriteProductService ICRUDFavoriteProductService)
        {
            _ICRUDFavoriteProductService = ICRUDFavoriteProductService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllFavoriteProducts()
        {
            var result = await _ICRUDFavoriteProductService.GetAllFavoriteProduct();
            return Ok(result);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateFavoritePR(CreateFavoriteProducts createfavoriteproducts)
        {
            await _ICRUDFavoriteProductService.CreateFavoriteProduct(createfavoriteproducts);
            return Ok(createfavoriteproducts);
        }
        [HttpGet]
        [Route("GetFPById/{idacc}")]
        public async Task<IActionResult> GetFPById(Guid idacc)
        {
            var size = await _ICRUDFavoriteProductService.GetFPById(idacc);
            return Ok(size);
        }

        [HttpGet]
        [Route("Delete/{idacc},{idproduct}")]
        public async Task<IActionResult> DeleteFavoriteProduct( Guid idacc , Guid idproduct)
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
