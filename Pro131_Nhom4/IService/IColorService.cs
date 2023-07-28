using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface IColorService
    {
        public Task<bool> CreateColor(CreateColor color);
        public Task<bool> UpdateColor(UpdateColor color);
        public Task<bool> DeleteColor(Guid id);
        public Task<Colors> GetColorById(Guid id);
        public Task<List<Colors>> GetColorByName(string name);
        public Task<List<Colors>> GetAllColor();
    }
}
