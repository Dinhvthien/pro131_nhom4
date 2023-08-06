using App_Shared.Model;
using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface IBillDetailsService
    {
        public Task<bool> CreateBillDetails(BillDetails address);
        public Task<bool> UpdateBillDetails(BillDetailsView address);
        public Task<bool> DeleteBillDetails(Guid id);
        public Task<List<BillDetailsView>> GetBillDetailsByBillId(Guid id);
        public Task<List<BillDetails>> GetAllBillDetails();
    }
}
