using BookstoreApplication.Models;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                 
                var created = await userService.Register(User);
                return created;

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
