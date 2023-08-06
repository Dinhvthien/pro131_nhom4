using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface ICartDetailsService
    {
        public Task<bool> CreateCartDetails(CartDetails cart);
        public Task<bool> Increase(Guid id);
        public Task<bool> Reduce(Guid id);
        public Task<bool> DeleteCartDetails(Guid id);
        public Task<CartDetailsView> GetCartDetailsById(Guid accountId, Guid productId);
        public Task<List<CartDetails>> GetCartDetailsByAccountId(Guid id);
        public Task<List<CartDetails>> GetAllCartDetails();
    }
}
