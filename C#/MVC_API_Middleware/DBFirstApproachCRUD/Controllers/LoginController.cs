using DBFirstApproachCRUD.Context;
using DBFirstApproachCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DBFirstApproachCRUD.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public IConfiguration _configuration;
        private readonly StudentDbContext _context;

        public LoginController(IConfiguration config, StudentDbContext context)
        {
            _configuration = config;
            _context = context;
        }


        //SingleOrDefault it returns a single, specific element of a sequence, or a default value
            // if that element is not found.
        // Whereas FirstOrDefault returns the first element of a sequence, or a default value if no element is found.

        private async Task<Student> GetStudent(string StudentName, string Password)
        {
            return await _context.Students.FirstOrDefaultAsync(u => u.StudentName == StudentName && u.Password == Password);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string StudentName, string Password)
        {

            if (StudentName == null && Password == null)
            {
                return BadRequest("Invalid client request");
            }
            Student student = await GetStudent(StudentName, Password);

            if (student != null)
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
