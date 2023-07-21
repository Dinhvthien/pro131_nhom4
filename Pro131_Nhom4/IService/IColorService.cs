using App_Shared.Model;

namespace Pro131_Nhom4.IService
{
    public interface IColorService
    {
        public Task<bool> CreateColor(Color color);
        public Task<bool> UpdateColor(Color color);
        public Task<bool> DeleteColor(Guid id);
        public Task<Color> GetColorById(Guid id);
        public Task<List<Color>> GetColorByName(string name);
        public Task<List<Color>> GetAllColor();
    }
}
