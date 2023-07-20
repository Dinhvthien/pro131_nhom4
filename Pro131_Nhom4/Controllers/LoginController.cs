using App_Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _loginService.LoginAsync(loginUser);
            if (result.IsSuccess)
            {
                return Ok(result.Token);
            }
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
