using App_Shared.Model;

namespace Pro131_Nhom4.IService
{
    public interface IPayment_typeService
    {
        public Task<bool> Createpayment(Payment payment);
        public Task<bool> UpdatePayment(Payment payment);
        public Task<bool> DeletePayment(Guid id);
        public Task<Payment> GetPaymentById(Guid id);
        public Task<List<Payment>> GetAllPayment();
    }
}
