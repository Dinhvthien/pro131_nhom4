using App_Shared.Model;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class Payment_typeService :IPayment_typeService
    {
        Mydb _context;
        public Payment_typeService()
        {
            _context = new Mydb();
        }
        public async Task<bool> Createpayment(Payment payment)
        {
            if (payment == null) return false;
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePayment(Guid id)
        {
            try
            {
                var del = _context.Payments.Find(id);
                _context.Payments.Remove(del);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<Payment>> GetAllPayment()
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetPaymentById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePayment(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
