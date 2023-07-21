using App_Shared.Model;

namespace Pro131_Nhom4.IService
{
    public interface IRankService
    {
        public Task<bool> CreateRank(Rank address);
        public Task<bool> UpdateRank(Rank address);
        public Task<bool> DeleteRank(Guid id);
        public Task<Rank> GetRankById(Guid id);
        public Task<List<Rank>> GetRankByName(string name);
        public Task<List<Rank>> GetAllRank();
    }
}
