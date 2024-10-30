using BookstoreApplication.Context;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookstoreApplication.Controllers
{
    [Route("api/User/Login")]
    [ApiController]
    public class LoginController : Controller
    {
        public IConfiguration _configuration;
        private readonly BookDBContext _context;
        public LoginController(IConfiguration config, BookDBContext context)
        {
            _configuration = config;
            _context = context;
        }
        private async Task<User> GetUser(string Username, string Password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password );
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel LoginModel)
        {
            if (LoginModel.Username == null && LoginModel.Password == null )
            {
                return BadRequest("Invalid client request");
            }
            User User = await GetUser(LoginModel.Username, LoginModel.Password);
            if (User != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var tokenDescriptor = new SecurityTokenDescriptor 
                { 
                    Subject = new ClaimsIdentity(
                        new Claim[] {
                        new Claim(ClaimTypes.Name, User.Username),
                        new Claim(ClaimTypes.Role, User.RoleId.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, User.UserId.ToString()),
                        new Claim("Roles", User.RoleId.ToString()),
                        new Claim("Role", User.RoleId.ToString()),
                        }),
                    Expires = DateTime.UtcNow.AddMinutes(55),
                    SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256)
                    };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                
                return Ok(new { message = "Login successfully" ,Token = tokenString, RoleId= (int)User.RoleId });
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
    }
}

