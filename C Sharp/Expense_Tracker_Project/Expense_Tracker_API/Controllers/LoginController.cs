using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserContext context;
        private readonly IConfiguration _config;

        public LoginController(UserContext context, IConfiguration _config)
        {
            this.context = context;
            this._config = _config;
        }

        //Login METHOD
        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromHeader]string UserName,[FromHeader] string Password)
        {
            Users user = context.Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (user != null)
            {
                var token = await GenerateToken(user);
                return Ok(new { Token = token });

               //return Ok(new { Token = token, Message = "Login Success" });
            }
            return Unauthorized();
        }

        //To Generate JWT token
        private async Task<string> GenerateToken(Users Users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]  {
            new Claim(ClaimTypes.NameIdentifier,Users.UserName),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(11).AddMinutes(10),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
