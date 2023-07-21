using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;

namespace Pro131_Nhom4.Services
{
    public class FavoriteProductsService
    {
        Mydb _context;
        public FavoriteProductsService()
        {
            _context = new Mydb();

        }
        public async Task<List<FavoriteProductsView>> GetFavoriteProduct(Guid accountId, Guid productId)
        {
            List<FavoriteProductsView> lstfavoriteProductsView = new List<FavoriteProductsView>();
            lstfavoriteProductsView = await
                (from a in _context.FavoriteProducts
                 join b in _context.Users on a.AccountID equals b.Id
                 join c in _context.Products on a.ProductID equals c.Id
                 where b.Id == accountId && c.Id == productId
                 select new FavoriteProductsView()
                 {
                     FavoriteProducts = a,
                     User = b,
                     Product = c
                 }).ToListAsync();
            return lstfavoriteProductsView;
        }

        public async Task<List<FavoriteProductsView>> GetFavoriteProductsByAccount(Guid accountId)
        {
            List<FavoriteProductsView> lstfavoriteProductsView = new List<FavoriteProductsView>();
            lstfavoriteProductsView = await
                (from a in _context.FavoriteProducts
                 join b in _context.Users on a.AccountID equals b.Id
                 join c in _context.Products on a.ProductID equals c.Id
                 where a.AccountID == accountId
                 select new FavoriteProductsView()
                 {
                     FavoriteProducts = a,
                     User = b,
                     Product = c
                 }).ToListAsync();
            return lstfavoriteProductsView;
        }

        public async Task<bool> Like(Guid accountId, Guid productId)
        {
            try
            {
                var favoriteProducts = _context.FavoriteProducts.Find(accountId, productId);
                if (favoriteProducts == null)
                {
                    FavoriteProducts favorite = new FavoriteProducts();
                    favorite.AccountID = accountId;
                    favorite.ProductID = productId;
                    favorite.Description = "Success";
                    var n = _context.Products.Find(productId);
                    n.Likes++;
                    _context.Products.Update(n);
                    await _context.FavoriteProducts.AddAsync(favorite);
                }
                else
                {
                    var n = _context.Products.Find(productId);
                    n.Likes--;
                    _context.Products.Update(n);
                    _context.FavoriteProducts.Remove(favoriteProducts);
                }
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
