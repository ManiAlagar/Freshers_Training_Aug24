using CRUD_Web_API.Entity;
using CRUD_Web_API.Entity.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
            private readonly UserContext context;

            public UserController(UserContext context)
            {
                this.context = context;
            }

          
            // GET METHOD
            [HttpGet("GetAll")]
            public IActionResult Get()
            {
               return  Ok(context.User.ToList());
            }

            //Get By ID METHOD
            [HttpGet("GetByID")]
            public  IActionResult Get(int id)
            {
                Users user = context.User.FirstOrDefault(u => u.ID == id);
                if(user == null)
                {
                    return NotFound("Record Not Found");
                }
                
                return Ok(user);
            }
            


            [HttpPost("Create")]
            public IActionResult Create([FromBody] Users user)
            {
                if (user == null)
                {
                    return BadRequest("Employee is null");
                }
                 
                context.User.Add(user);
                
                context.SaveChanges();

                return Ok(user + "Created Successfuly");
            }

            [HttpPut("{id}")]
            public IActionResult update(int id, [FromBody] Users entity)
            {              
                Users user = context.User.FirstOrDefault(u => u.ID == id);

                if (user == null)
                {
                    return NotFound("The Employee record couldn't be found.");
                }

                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.Email = entity.Email;
                user.PhoneNumber = entity.PhoneNumber;
               
                context.Update(user);
                context.SaveChanges();

                return Ok("Updated Successful");
            }


            //DELETE METHOD
            [HttpDelete("Delete/{id}")]
            public IActionResult Delete(int id)
            {
                Users user = context.User.FirstOrDefault(u => u.ID == id);

                if (user == null)
                {
                    return NotFound("The Employee record couldn't be found.");
                }
                
                context.User.Remove(user);
                context.SaveChanges();
                return Ok();
            }
    }
}

