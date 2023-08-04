using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;
using System.Net;
using System.Net.WebSockets;

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

        public async Task<List<ViewFavoriteProduct>> GetAllFavoriteProduct()
        {
            List<ViewFavoriteProduct> lst = await
              (from a in _context.FavoriteProducts
               join b in _context.Products on a.ProductID equals b.Id
               join c in _context.Users on a.AccountID equals c.Id
               select new ViewFavoriteProduct()
               {
                   AccountID = a.AccountID,
                   ProductID =a.ProductID,
                   Description = a.Description,
                   NameProduct = b.Name,
                   AvailableQuantityProduct = b.AvailableQuantity,
                   DescriptionProduct = b.Description,
                   ImageUrlProduct = b.ImageUrl,
                   LikesProduct = b.Likes,
                   ManufacturerProduct = b.Manufacturer,
                   PriceProduct = b.Price,
                   StatusProduct = b.Status
               }).ToListAsync();
            return lst;
        }

        public async Task<ViewFavoriteProduct> GetFPById(Guid idacc)
        {
            ViewFavoriteProduct lst = new ViewFavoriteProduct();
            lst = await

              (from a in _context.FavoriteProducts
               join b in _context.Products on a.ProductID equals b.Id
               where a.AccountID == idacc 
               select new ViewFavoriteProduct()
               {
                   AccountID = a.AccountID,
                   ProductID = a.ProductID,
                   Description = a.Description,
                   NameProduct = b.Name,
                   AvailableQuantityProduct = b.AvailableQuantity,
                   DescriptionProduct = b.Description,
                   ImageUrlProduct = b.ImageUrl,
                   LikesProduct = b.Likes,
                   ManufacturerProduct = b.Manufacturer,
                   PriceProduct = b.Price,
                   StatusProduct = b.Status
               }).FirstAsync();
            return lst;
        }
    }
}
