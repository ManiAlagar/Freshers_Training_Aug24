using BookstoreApplication.Context;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password);
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {

            if (user.Username == null && user.Password == null)
            {
                return BadRequest("Invalid client request");
            }
            User User = await GetUser(user.Username, user.Password);

            if (User != null)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//HMAC is a keyed hash that can be used with any hash algorithm
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return BadRequest("Invalid credentials");
            }


        }
    }
}
