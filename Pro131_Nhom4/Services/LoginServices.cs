using App_Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pro131_Nhom4.Services
{
    public class LoginServices : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public LoginServices(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<Response> LoginAsync(LoginUser loginUser)
        {

            // Check user is exists
            var user = await _userManager.FindByNameAsync(loginUser.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                // Get list roles of user
                var roles = await _userManager.GetRolesAsync(user);

                // Create list of claims
                var claims = new List<Claim>()
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                };

                // Create JWT Token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configuration["JWT:Issuer"]
                    , _configuration["JWT:Audience"], claims, expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: signIn);

                return new Response()
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Valid user",
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
            else
            {
                return new Response()
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Invalid user"
                };
            }
        }
    }
}
