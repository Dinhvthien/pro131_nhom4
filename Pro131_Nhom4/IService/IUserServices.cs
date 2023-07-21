using App_Shared.ViewModels;
using Pro131_Nhom4.Data;
using System.Security.Principal;

namespace Pro131_Nhom4.IService
{
    public interface IUserServices
    {
        public Task<bool> UpdateAccountPoint(UserView account);
        public Task<bool> UpdateAccount(UpdateUser account);
        public Task<UserView> GetAccountById(Guid id);
        public Task<List<User>> GetAllAccounts();
    }
}
