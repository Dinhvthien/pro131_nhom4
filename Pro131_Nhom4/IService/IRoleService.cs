using Pro131_Nhom4.Data;

namespace Pro131_Nhom4.IService
{
    public interface IRoleService
    {
        public Task<bool> CreateRole(Role role);
        public Task<bool> UpdateRole(Role role);
        public Task<bool> DeleteRole(Guid id);
        public Task<Role> GetRoleById(Guid id);
        public Task<Role> GetRoleByName(string name);
        public Task<List<Role>> GetAllRole();
    }
}
