using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;
using System.Drawing;

namespace Pro131_Nhom4.Services
{
    public class SizeService : ISizeService
    {
        Mydb _context;
        public SizeService()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateSize(CreateSize address)
        {
            if (address == null) return false;
            Sizes size = new Sizes() { 
            Id = address.Id,
            Name = address.Name,
            Status = address.Status,
            Products = null
            };
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSize(Guid id)
        {
            try
            {
                var delsize = _context.Sizes.Find(id);
                _context.Sizes.Remove(delsize);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Sizes>> GetAllSize()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<Sizes> GetSizeById(Guid id)
        {
            return await _context.Sizes.AsQueryable().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Sizes>> GetSizeByName(string name)
        {
            return await _context.Sizes.AsQueryable().Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<bool> UpdateSize(UpdateSize address)
        {
            try
            {
                var s = _context.Sizes.Find(address.Id);
                s.Name = address.Name;
                s.Status = address.Status;
                _context.Update(s);
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
