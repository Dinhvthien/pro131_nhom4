using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class BillServices : IBillService
    {
        Mydb _context;
        public BillServices()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateBill(Bill address)
        {
            if (address == null) return false;
            await _context.Bills.AddAsync(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBill(Guid id)
        {
            try
            {
                var del = _context.Bills.Find(id);
                _context.Bills.Remove(del);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Bill>> GetAllBills()
        {
            var a = await _context.Bills.ToListAsync();
            return a;
        }

        public async Task<List<BillView>> GetBillByAccountId(Guid id)
        {
            List<BillView> lstBillViews = new List<BillView>();
            lstBillViews = await (
                from a in _context.Bills
                join b in _context.Vouchers on a.VoucherID equals b.Id
                join c in _context.Users on a.AccountID equals c.Id
                join d in _context.Payments on a.Id equals d.Id
                join e in _context.BillStatuses on a.Id equals e.IdStt

                select new BillView()
                {
                    Bill = a,
                    Voucher = b,
                    User = c,
                    Payment = d,
                    BillStatus = e,
                }).ToListAsync();
            UserView accountView = new UserView();
            accountView = await (
                from a in _context.Users
                join b in _context.Roles on a.RoleId equals b.Id
                join c in _context.Ranks on a.RankID equals c.Id
                where a.Id == id
                select new UserView()
                {
                    User = a,
                    Role = b,
                    Rank = c
                }).FirstAsync();
            if (accountView.Role.Id == Guid.Parse(""))
            {
                return lstBillViews;
            }
            return lstBillViews.Where(p => p.User.Id == id).ToList();
        }

        public async Task<BillView> GetBillById(Guid id)
        {
            List<BillView> billViews = new List<BillView>();
            billViews = await (
                from a in _context.Bills
                join b in _context.Vouchers on a.VoucherID equals b.Id
                join c in _context.Users on a.AccountID equals c.Id
                join d in _context.Payments on a.Id equals d.Id
                join e in _context.BillStatuses on a.Id equals e.IdStt

                select new BillView()
                {
                    Bill = a,
                    Voucher = b,
                    User = c,
                    Payment = d,
                    BillStatus = e,
                }).ToListAsync();
            return billViews.FirstOrDefault(p => p.Bill.Id == id);
        }

        public async Task<bool> UpdateBill(Bill address)
        {
            try
            {
                var up = _context.Bills.Find(address.Id);
            /*    up.Price = address.Price;
                up.CreateDate = address.CreateDate;*/
                up.StatusID = address.StatusID;
                //up.Voucher = address.Bill.Voucher;
                _context.Bills.Update(up);
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
