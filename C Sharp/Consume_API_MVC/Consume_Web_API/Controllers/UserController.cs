using DB_First_Approach.Entity;
using DB_First_Approach.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DB_First_Approach.Controllers
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Users> user = await userService.Get();
            return Ok(user);
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> Get(int id)
        {
            Users user = await userService.Get(id); 
            if (user != null)
            {
                return Ok(user);

            }
            return NotFound("Record Not Found");
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("Employee is null");
            }

            await userService.Add(user);

            return Ok(user);
        }

        
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Users entity)
        {
            Users user = await userService.Get(id);

            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            await userService.Edit(entity);

            return Ok("Updated Successful");
        }

        // DELETE METHOD
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Users user = await userService.Get(id);
          
            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            await userService.Delete(user);

            return Ok("Record Deleted");
        }
    }
}
