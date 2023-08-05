using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
		private readonly ICartService _cartService;
		public RegisterController(IRegisterService registerService, ICartService cartService)
        {
            _registerService = registerService;
			_cartService = cartService;

		}

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser, string role)
        {
            var result = await _registerService.RegisterAsync(registerUser, role);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
