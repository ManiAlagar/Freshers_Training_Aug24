using BookstoreApplication.Models;
using BookstoreApplication.Service.Implementation;
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
        public async Task<ActionResult<int>> Register(User User)
        {
            try
            {
                if (User == null)
                    return BadRequest();
                 
                var result = await userService.Register(User);
                var res = new ApiResponse<int>("Registered successfully", 200, result);
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

        [HttpPut]
        [Route("UpdateUser/{id:int}")]
        public async Task<ActionResult<int?>> UpdateUser([FromRoute] int id, User user)
        {
            try
            {
                if (id == null)
                    return BadRequest("user ID mismatch");

                var ToUpdate = await userService.GetUserById(id);

                if (ToUpdate == null)
                    return NotFound($"Book with Id = {id} not found");

                var updated = await userService.UpdateUser(id, user);
                var res = new ApiResponse<int>("Updated successfully", 200, updated);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }


    }
}
