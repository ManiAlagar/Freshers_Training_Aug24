using CRUD_Web_API.Entity;
using CRUD_Web_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CRUD_Web_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;


        public UserController(IUserService userService)
        {
          
            this.userService = userService;
        }

          
        // GET METHOD
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            IEnumerable<Users> user = userService.GetAll();
            return Ok(user);
        }


        //Get By ID METHOD
        [HttpGet("GetByID")]
        public  IActionResult Get(int id)
        {
            Users user = userService.Get(id);
            if(user != null)
            {
                return Ok(user);
              
            }
            return NotFound("Record Not Found");    
        }
            

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("Employee is null");
            }

            userService.Add(user);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult update(int id, [FromBody] Users entity)
        {
               
            Users user = userService.Get(id);
            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            userService.Edit(user, entity);

            return Ok("Updated Successful");
        }


        // DELETE METHOD
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            
            Users user = userService.Get(id);
            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
               
            return Ok("Record Deleted");
        }
    }
}

