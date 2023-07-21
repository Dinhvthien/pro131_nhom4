using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface ICartService
    {
        public Task<bool> CreateCart(Cart cart);
        public Task<bool> UpdateCart(CartView cart);
        public Task<bool> DeleteCart(Guid id);
        public Task<CartView> GetCartById(Guid id);
        public Task<List<CartView>> GetAllCart();
    }
}
