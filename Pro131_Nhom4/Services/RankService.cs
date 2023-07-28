using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class RankService : IRankService
    {
        Mydb _context;
        public RankService()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateRank(CreateRank createRank)
        {
            if (createRank == null) return false;
            Rank rank = new Rank()
            {
                Id = createRank.Id,
                Name = createRank.Name,
                Point = createRank.Point,
                User = null
            };
            await _context.Ranks.AddAsync(rank);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRank(Guid id)
        {
            try
            {
                var delrank = _context.Ranks.Find(id);
                _context.Ranks.Remove(delrank);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Rank>> GetAllRank()
        {
            return await _context.Ranks.ToListAsync();
        }

        public async Task<Rank> GetRankById(Guid id)
        {
            return await _context.Ranks.AsQueryable().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Rank>> GetRankByName(string name)
        {
            return await _context.Ranks.AsQueryable().Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<bool> UpdateRank(UpdateRank updateRank)
        {
            try
            {
                var n = _context.Ranks.Find(updateRank.Id);
                n.Name = updateRank.Name;
                n.Point = updateRank.Point;
                _context.Update(n);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
