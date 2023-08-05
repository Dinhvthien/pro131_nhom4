using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class CartService : ICartService
    {
        Mydb _context;
        public CartService()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateCart(Cart address)
        {
            if (address == null) { return false; } else {
            await _context.Carts.AddAsync(address);
            await _context.SaveChangesAsync();
				return true;
			}
		}
			

        public async Task<bool> DeleteCart(Guid id)
        {
            try
            {
                var del = _context.Carts.Find(id);
                _context.Carts.Remove(del);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<CartView>> GetAllCart()
        {
            List<CartView> cartView = new List<CartView>();
            cartView = await (
                from a in _context.Carts
                join b in _context.Users on a.UserID equals b.Id
                select new CartView()
                {
                    Cart = a,
                    User = b,
                }).ToListAsync();
            return cartView;
        }

        public async Task<CartView> GetCartById(Guid id)
        {
            List<CartView> cartView = new List<CartView>();
            cartView = await (
                from a in _context.Carts
                join b in _context.Users on a.UserID equals b.Id
                select new CartView()
                {
                    Cart = a,
                    User = b,
                }).ToListAsync();
            return cartView.FirstOrDefault(p => p.Cart.UserID == id);
        }

        public async Task<bool> UpdateCart(CartView address)
        {
            try
            {
                var up = _context.Carts.Find(address.Cart.UserID);
                up.Description = address.Cart.Description;
                _context.Carts.Update(up);
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
