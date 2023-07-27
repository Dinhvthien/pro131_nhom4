using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.CodeAnalysis;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;
using System.Net;

namespace Pro131_Nhom4.Services
{
    public class CRUDFavoriteProductService : ICRUDFavoriteProductService
    {
        Mydb _context;
        public CRUDFavoriteProductService()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateFavoriteProduct(CreateFavoriteProducts rq)
        {
            if (rq == null) return false;
            FavoriteProducts a = new FavoriteProducts()
            {
                AccountID = rq.AccountID,
                ProductID = rq.ProductID,
                Description = rq.Description,
                Products = null,
                User = null
            };
            await _context.FavoriteProducts.AddAsync(a);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFavoriteProduct(Guid idacc , Guid idproduts)
        {
            try
            {
                var deleteFavoriteProduct = _context.FavoriteProducts.FirstOrDefault(p => p.AccountID == idacc && p.ProductID == idproduts);
                
                _context.FavoriteProducts.Remove(deleteFavoriteProduct);               
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
