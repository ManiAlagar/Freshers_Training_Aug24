using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;


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
        public async Task<IActionResult> Login([FromHeader]string Email, [FromHeader] string? Password)
        {
            Users user = context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user != null)
            {
                var token = await GenerateToken(user);
                return Ok(new { Token = token });

               //return Ok(new { Token = token, Message = "Login Success" });
            }
            return Unauthorized();
        }

        //To Generate JWT token
        private async Task<string> GenerateToken(Users entity)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]  {
                new Claim("UserName",entity.UserName),
                new Claim("UserID",entity.UserID.ToString()),
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
