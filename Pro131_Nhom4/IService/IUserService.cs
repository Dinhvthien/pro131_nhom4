using App_Shared.ViewModels;
using Pro131_Nhom4.Data;

namespace Pro131_Nhom4.IService
{
	public interface IUserService
	{
		public Task<User> GetUserbyId(Guid id);
		public Task<List<User>> GetAllUser();
        public Task<bool> Updateuser(User user);
        public Task<string> UpdateRoleUserAsync(UserRoles p);
	}
}
