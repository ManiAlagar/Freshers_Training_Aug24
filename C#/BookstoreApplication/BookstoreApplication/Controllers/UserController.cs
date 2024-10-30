using BookstoreApplication.Models;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(User User)
        {
            try
            {
                if (User == null)
                    return BadRequest();
                 
                var result = await userService.Register(User);
                var res = new ApiResponse<User>("Registered successfully", 200, result);
                return Ok(res);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }

        [HttpGet]
        [Route("GetUserById/{id:int}")]
        public async Task<ActionResult<User?>> GetUserById([FromRoute] int id)

        {
            try
            {
                var result = await userService.GetUserById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
