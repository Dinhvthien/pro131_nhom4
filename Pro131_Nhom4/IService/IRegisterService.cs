using App_Shared.ViewModels;

namespace Pro131_Nhom4.IService
{
    public interface IRegisterService
    {
        Task<Response> RegisterAsync(RegisterUser registerUser, string role);
    }
}
