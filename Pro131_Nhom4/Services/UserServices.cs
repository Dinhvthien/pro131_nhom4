using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class UserServices : IUserServices
    {
        Mydb _context;
        public UserServices()
        {
            _context = new Mydb();
        }
        public async Task<UserView> GetAccountById(Guid id)
        {
            List<UserView> lstAccountView = new List<UserView>();
            lstAccountView = await (
                from a in _context.Users
                join b in _context.Roles on a.RoleId equals b.Id
                join c in _context.Ranks on a.RankID equals c.Id
                where a.Id == id
                select new UserView()
                {
                    User = a,
                    Role = b,
                    Rank = c
                }).ToListAsync();
            return lstAccountView.AsQueryable().Where(p => p.User.Id == id).FirstOrDefault();
        }

        public async Task<List<User>> GetAllAccounts()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> UpdateAccount(UpdateUser account)
        {
            try
            {
                var n = _context.Users.Find(account.Id);
                n.UserName = account.UserName;
                n.DateOfBirth = account.DateOfBirth;
                n.Gender = account.Gender;
                n.PhoneNumber = account.PhoneNumber;
                n.Email = account.Email;
                n.Address = account.Address;
                _context.Users.Update(n);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAccountPoint(UserView account)
        {
            try
            {
                var n = _context.Users.Find(account.User.Id);
                n.Point = account.User.Point;
                if (account.User.Point >= 2003)
                {
                    n.RankID = Guid.Parse("");
                }
                else if (account.User.Point >= 1000)
                {
                    n.RankID = Guid.Parse("");
                }
                else if (account.User.Point >= 500)
                {
                    n.RankID = Guid.Parse("");
                }
                _context.Users.Update(n);
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
