using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface ILoginService
    {
        Task<Response> LoginAsync(LoginUser loginUser);
    }
}
