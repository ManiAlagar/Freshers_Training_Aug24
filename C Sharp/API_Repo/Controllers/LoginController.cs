using CRUD_Web_API.Entity;
using CRUD_Web_API.Entity.DBContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUD_Web_API.Controllers
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
        public async Task<IActionResult> Login(string UserName,string Password)
        {
            Users user = context.User.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(new { Token = token, Message = "Login Success" });
            }
            return Unauthorized(); 
        }

        // To Generate JWT token
        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
            };
            
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(11).AddMinutes(10),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Authorize the Method       
        [HttpGet(nameof(UserAuthorizing))]
        public async Task<IEnumerable<string>> UserAuthorizing()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return new string[] { accessToken };
        }

    }
}

