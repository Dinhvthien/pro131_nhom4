using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface IProductService
    {
        public Task<bool> CreateProduct(Product product);
        public Task<bool> UpdateProduct(ProductView product);
		public Task<bool> UpdateProduct2(Guid id, int slsp);
		public Task<bool> DeleteProduct(Guid id);
        public Task<ProductView> GetProductById(Guid id);
        public Task<Product> GetProductByCS(string name, Guid colorId, Guid sizeId);
        public Task<List<ProductView>> GetProductByName(string name);
        public Task<List<ProductView>> ShowListProduct();
    }
}
