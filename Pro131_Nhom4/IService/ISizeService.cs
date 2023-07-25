using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface ISizeService
    {
        public Task<bool> CreateSize(CreateSize createSize);
        public Task<bool> UpdateSize(UpdateSize address);
        public Task<bool> DeleteSize(Guid id);
        public Task<Sizes> GetSizeById(Guid id);
        public Task<List<Sizes>> GetSizeByName(string name);
        public Task<List<Sizes>> GetAllSize();
    }
}
