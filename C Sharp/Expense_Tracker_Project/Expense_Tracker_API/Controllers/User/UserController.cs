using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers.User
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
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
            //IEnumerable<Users> user = await userService.Get();
            return Ok(await userService.Get());
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

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Users user)
        {

            if (user == null)
            {
                return BadRequest("Employee is null");
            }
            bool flag = await userService.Add(user);

            if (flag)
                return Ok();

            return BadRequest();
        }


        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Users entity)
        {
            Users user = await userService.Get(id);

            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            entity.UserID = id;
            bool status = await userService.Edit(entity);

            return Ok(status);
        }
    }
}