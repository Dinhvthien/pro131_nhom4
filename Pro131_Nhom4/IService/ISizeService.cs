using App_Shared.Model;

namespace Pro131_Nhom4.IService
{
    public interface ISizeService
    {
        public Task<bool> CreateSize(Size address);
        public Task<bool> UpdateSize(Size address);
        public Task<bool> DeleteSize(Guid id);
        public Task<Size> GetSizeById(Guid id);
        public Task<List<Size>> GetSizeByName(string name);
        public Task<List<Size>> GetAllSize();
    }
}
