using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface ICRUDFavoriteProductService
    {
        public Task<bool> CreateFavoriteProduct(CreateFavoriteProducts rq);
        //public Task<bool> UpdateFavoriteProduct(UpdateFavoriteProducts rq);
        public Task<bool> DeleteFavoriteProduct(Guid idacc , Guid idproduct);
        public Task<List<ViewFavoriteProduct>> GetAllFavoriteProduct();
        public Task<ViewFavoriteProduct> GetFPById(Guid idacc);
    }
}
