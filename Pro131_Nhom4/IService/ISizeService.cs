using App_Shared.Model;

namespace Pro131_Nhom4.IService
{
    public interface ISizeService
    {
        public Task<bool> CreateSize(Sizes address);
        public Task<bool> UpdateSize(Sizes address);
        public Task<bool> DeleteSize(Guid id);
        public Task<Sizes> GetSizeById(Guid id);
        public Task<List<Sizes>> GetSizeByName(string name);
        public Task<List<Sizes>> GetAllSize();
    }
}
