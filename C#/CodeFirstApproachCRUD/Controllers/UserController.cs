using CodeFirstApproachCRUD.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachCRUD.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        public readonly UserDbContext db;
        public UserController(UserDbContext db)
        {
            this.db = db;
        }
        [HttpGet("GetAll")]
        
        public IActionResult GetAll()
        {
            return Ok(db.Users.ToList());
        }

        [HttpPost("CreateUser")]
        public IActionResult AddUser(User User)
        {
            var newUser = new User()
            {
                UserName = User.UserName,
                UserEmail = User.UserEmail,
                UserPhone = User.UserPhone,
                UserPassword = User.UserPassword,

            };
            db.Users.Add(newUser);
            db.SaveChanges();
            return Ok(newUser);
        }

        [HttpGet]
        [Route("GetByUserID/{id:int}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            var user =  db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut] 
        [Route("UpdateUser/{id:int}")]
        public IActionResult UpdateUser([FromRoute] int id,  User User)
        {
            var existingUser = db.Users.Find(id);
            if (existingUser != null)
            {
                existingUser.UserName = User.UserName;
                existingUser.UserEmail = User.UserEmail;
                existingUser.UserPhone = User.UserPhone;
                existingUser.UserPassword = User.UserPassword;
                 db.SaveChangesAsync();
                return Ok(existingUser);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("DeleteUser/{id:int}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var DeleteUser = db.Users.Find(id);
            if (DeleteUser != null)
            {
                db.Remove(DeleteUser);
                db.SaveChanges();
                return Ok(DeleteUser);
            }
            return NotFound();
        }
    }
    
}
