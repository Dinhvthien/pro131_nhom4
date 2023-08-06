using App_Shared.Model;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class VoucherServices : IVoucherService
    {
        Mydb _context;
        public VoucherServices()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateVoucher(Voucher address)
        {
            if (address == null) return false;
            await _context.Vouchers.AddAsync(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVoucher(Guid id)
        {
            try
            {
                var delvc = _context.Vouchers.Find(id);
                _context.Vouchers.Remove(delvc);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Voucher>> GetAllVoucher()
        {
            return await _context.Vouchers.ToListAsync();
        }

        public async Task<Voucher> GetVoucherById(Guid id)
        {
            return await _context.Vouchers.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Voucher>> GetVoucherByName(string name)
        {
            return await _context.Vouchers.Where(p => p.VoucherName.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<bool> UpdateVoucher(Voucher address)
        {
            try
            {
                var upvc = _context.Vouchers.Find(address.Id);
                upvc.VoucherName = address.VoucherName;
                upvc.PercenDiscount = address.PercenDiscount;
                upvc.TimeEnd = address.TimeEnd;
                upvc.TimeStart = address.TimeStart;
                upvc.Status = address.Status;
                _context.Update(upvc);
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
