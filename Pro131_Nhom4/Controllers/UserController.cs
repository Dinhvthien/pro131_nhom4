using App_client.Services;
using App_Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService UserService;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		public UserController(IUserService userService, UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			UserService = userService;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		[HttpGet]
		public async Task<IActionResult> ShowListUser()
		{
			var User = await UserService.GetAllUser();
			return Ok(User);
		}

			[HttpGet("{id:Guid}")]
		public async Task<IActionResult> ShowUserbyId(Guid id)
		{
			var User = await UserService.GetUserbyId(id);
			return Ok(User);
		}

		[HttpPost("id")]
		public async Task<IActionResult> ChangeUserRole(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
			{
				return BadRequest();
			}
			// Xóa vai trò "User" (nếu có) và thêm vai trò "Admin" cho người dùng
			await _userManager.RemoveFromRoleAsync(user, "User");
			await _userManager.AddToRoleAsync(user, "Admin");

			// Xử lý thành công, chẳng hạn chuyển hướng người dùng đến trang quản trị
			return Ok();
		}

		[HttpPut]
        [HttpPut]
        public async Task<ActionResult<CartView>> UpdateUSer(User user)
        {
            await UserService.Updateuser(user);
            return Ok(user);
        }
    }
}
