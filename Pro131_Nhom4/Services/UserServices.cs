using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;
using System.Net;

namespace Pro131_Nhom4.Services
{
	public class UserServices : IUserService
	{
		Mydb _context;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		public UserServices(UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			_context = new Mydb();
			_userManager = userManager;
			_roleManager = roleManager;

		}

		public async Task<List<User>> GetAllUser()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<User> GetUserbyId(Guid id)
		{
			return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<string> UpdateRoleUserAsync(UserRoles p)
		{
			try
			{
				var findUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == p.iduser);

				//var userRoles = await _userManager.GetRolesAsync(findUser);
				//await _userManager.RemoveFromRolesAsync(findUser, userRoles);
				//// Gán vai trò mới cho người dùng
				//await _userManager.AddToRoleAsync(findUser, p.Role);

				return "10 dime";
			}
			catch (Exception)
			{
				return "0 dime";
			}
		}


        public async Task<bool> Updateuser(User user)
        {
            try
			{
                 _context.Users.Update(user);
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
