using CodeFirstApproachCRUD.Context;
using CodeFirstApproachCRUD.Repository.Interface;
using CodeFirstApproachCRUD.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachCRUD.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]

    public class UserController : ControllerBase 
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await userService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> AddUser( User User)
        {
            try
            {
                if (User == null)
                    return BadRequest();

                var created = await userService.AddUser(User);
                return created;
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }

        [HttpGet]
        [Route("GetByUserID/{id:int}")]
        public async Task<ActionResult<User?>> GetUser([FromRoute] int id)

        {
            try
            {
                var result = await userService.GetUser(id);

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
        public async Task<ActionResult<User?>> UpdateUser([FromRoute] int id,  User User)
        {
            try
            {
                if (id == null)
                    return BadRequest("User ID mismatch");

                var userToUpdate = await userService.GetUser(id);

                if (userToUpdate == null)
                    return NotFound($"User with Id = {id} not found");

                return await userService.UpdateUser(id , User);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{id:int}")]
        public async Task<ActionResult<User?>> DeleteUser([FromRoute] int id)
        {
            try
            {
                var userToDelete = await userService.GetUser(id);

                if (userToDelete == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }
                return await userService.DeleteUser(id);



            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

       
        
    }
    
}

//UserRepository.DeleteUser(id);









//IActionResult -where a method may return various response types depending on different conditions.
//ActionResult  specify both the data type and the HTTP status code to be returned.