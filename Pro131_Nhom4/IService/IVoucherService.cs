using App_Shared.Model;

namespace Pro131_Nhom4.IService
{
    public interface IVoucherService
    {
        public Task<bool> CreateVoucher(Voucher voucher);
        public Task<bool> UpdateVoucher(Voucher voucher);
        public Task<bool> DeleteVoucher(Guid id);
        public Task<Voucher> GetVoucherById(Guid id);
        public Task<List<Voucher>> GetVoucherByName(string name);
        public Task<List<Voucher>> GetAllVoucher();
    }
}
