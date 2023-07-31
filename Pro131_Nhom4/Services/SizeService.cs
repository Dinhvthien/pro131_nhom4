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
            Id = Guid.NewGuid(),
            Name = address.Name,
            Status = address.Status
            //Products = null
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

        public async Task<List<ViewSize>> GetAllSize()
        {
            List<ViewSize> lst = await
              (from a in _context.Sizes
               select new ViewSize()
               {
                   Id = a.Id,
                   Name = a.Name,
                   Status = a.Status,                  
               }).ToListAsync();
            return lst;
        }

        public async Task<ViewSize> GetSizeById(Guid id)
        {
            ViewSize lst = new ViewSize();
            lst = await

              (from a in _context.Sizes 
               where a.Id == id
               select new ViewSize()
               {
                   Id = a.Id,
                   Name = a.Name,
                   Status = a.Status,
               }).FirstAsync();
            return lst;
        }

        public async Task<List<ViewSize>> GetSizeByName(string name)
        {
            List<ViewSize> lst = await
              (from a in _context.Sizes where a.Name == name
               select new ViewSize()
               {
                   //Id = a.Id,
                   Name = a.Name,

                   Status = a.Status,
               }).ToListAsync();
            return lst;
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
