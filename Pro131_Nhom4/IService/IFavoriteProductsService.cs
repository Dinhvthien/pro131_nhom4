using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface IFavoriteProductsService
    {
        public Task<bool> Like(Guid accountId, Guid productId);
        public Task<List<FavoriteProductsView>> GetFavoriteProductsByAccount(Guid accountId);
        public Task<List<FavoriteProductsView>> GetFavoriteProduct(Guid accountId, Guid productId);
    }
}
