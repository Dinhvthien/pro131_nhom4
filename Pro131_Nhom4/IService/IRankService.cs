using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface IRankService
    {
        public Task<bool> CreateRank(CreateRank createRank);
        public Task<bool> UpdateRank(UpdateRank updateRank);
        public Task<bool> DeleteRank(Guid id);
        public Task<Rank> GetRankById(Guid id);
        public Task<List<Rank>> GetRankByName(string name);
        public Task<List<Rank>> GetAllRank();
    }
}
