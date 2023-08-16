using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface IBillService
    {
        public Task<bool> CreateBill(Bill bill);
        public Task<bool> UpdateBill(Bill bill);
        public Task<bool> DeleteBill(Guid id);
        public Task<BillView> GetBillById(Guid id);
        public Task<List<BillView>> GetBillByAccountId(Guid id);
        public Task<List<Bill>> GetAllBills();
    }
}
