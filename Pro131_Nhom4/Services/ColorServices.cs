using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;
using System.Net;

namespace Pro131_Nhom4.Services
{
    public class ColorServices : IColorService
    {
        Mydb _context;
        public ColorServices()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateColor(CreateColor createcolor)
        {
            if (createcolor == null) return false;
            Colors color = new Colors()
            {
                Id = createcolor.Id,
                Name = createcolor.Name,
                Status = createcolor.Status,
                Products = null
            };
            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteColor(Guid id)
        {
            try
            {
                var color = _context.Colors.Find(id);
                _context.Colors.Remove(color);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Colors>> GetAllColor()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<Colors> GetColorById(Guid id)
        {
            return await _context.Colors.AsQueryable().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Colors>> GetColorByName(string name)
        {
            return await _context.Colors.AsQueryable().Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<bool> UpdateColor(UpdateColor color)
        {
            try
            {
                var n = _context.Colors.Find(color.Id);
                n.Name = color.Name;
                n.Status = color.Status;
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
