using App_Shared.Model;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class BillStatusServices : IBillStatusServices
    {
        Mydb _context;
        public BillStatusServices()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateBillStatusAsync(BillStatus p)
        {
            try
            {
                await _context.BillStatuses.AddAsync(p);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteBillStatusAsync(Guid id)
        {
            try
            {
                var billStatus = await _context.BillStatuses.FirstOrDefaultAsync(c => c.IdStt == id);
                _context.BillStatuses.Remove(billStatus);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<List<BillStatus>> GetAllBillStatusAsync()
        {
            return await _context.BillStatuses.ToListAsync();
        }

        public async Task<BillStatus> GetBillStatusByIdAsync(Guid id)
        {
            return await _context.BillStatuses.FirstOrDefaultAsync(c => c.IdStt == id);
        }



        public async Task<bool> UpdateBillStatusAsync(BillStatus p)
        {
            try
            {
                _context.BillStatuses.Update(p);
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
