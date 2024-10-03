using CodeFirstApproachCRUD.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeFirstApproachCRUD.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly UserDbContext _context;

        public TokenController(IConfiguration config, UserDbContext context)
        {
            _configuration = config;
            _context = context;
        }
       

        private Task<User> GetUser(string UserName, string UserPassword)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName && u.UserPassword == UserPassword);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string UserName, string UserPassword)
        {

            if (UserName == null && UserPassword == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = await GetUser(UserName, UserPassword);

                if (user != null)
                {

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
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
