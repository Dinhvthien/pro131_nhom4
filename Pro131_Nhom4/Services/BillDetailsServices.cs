using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class BillDetailsServices : IBillDetailsService
    {
        Mydb _context;
        public BillDetailsServices()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateBillDetails(BillDetails address)
        {
            if (address == null) return false;
            await _context.Billdetails.AddAsync(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBillDetails(Guid id)
        {
            try
            {
                var del = _context.Billdetails.Find(id);
                _context.Billdetails.Remove(del);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<BillDetails>> GetAllBillDetails()
        {

            return await _context.Billdetails.ToListAsync(); 
        }

        public async Task<List<BillDetailsView>> GetBillDetailsByBillId(Guid id)
        {
            List<BillDetailsView> billDetailsViews = new List<BillDetailsView>();
            billDetailsViews = await (
                from a in _context.Billdetails
                join b in _context.Products on a.ProductID equals b.Id
                join c in _context.Bills on a.BillID equals c.Id
                where a.BillID == id
                select new BillDetailsView()
                {
                    BillDetails = a,
                    Products = b,
                    Bill = c
                }).ToListAsync();
            return billDetailsViews;
        }

        public async Task<bool> UpdateBillDetails(BillDetailsView address)
        {
            try
            {
                var up = _context.Billdetails.Find(address.BillDetails.Id);
                up.Quantity = address.BillDetails.Quantity;
                up.Prices = address.BillDetails.Prices;
                _context.Update(up);
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
