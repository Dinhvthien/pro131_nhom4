using App_Shared.Model;

namespace Pro131_Nhom4.IService
{
    public interface IBillStatusServices
    {
        public Task<bool> CreateBillStatusAsync(BillStatus p);

        public Task<bool> UpdateBillStatusAsync(BillStatus p);
        public Task<bool> DeleteBillStatusAsync(Guid id);
        public Task<BillStatus> GetBillStatusByIdAsync(Guid id);

        public Task<List<BillStatus>> GetAllBillStatusAsync();
    }
}
