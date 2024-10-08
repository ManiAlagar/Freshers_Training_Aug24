using DBFirstApproachCRUD.Models;
using DBFirstApproachCRUD.Services.Implementation;
using DBFirstApproachCRUD.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBFirstApproachCRUD.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class StudentController: ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await studentService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<Student>> AddStudent(Student User)
        {
            try
            {
                if (User == null)
                    return BadRequest();

                var created = await studentService.AddStudent(User);
                return created;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }

        [HttpGet]
        [Route("GetByStudentID/{id:int}")]
        public async Task<ActionResult<Student?>> GetStudent([FromRoute] int id)

        {
            try
            {
                var result = await studentService.GetStudent(id);

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
        [Route("UpdateStudent/{id:int}")]
        public async Task<ActionResult<Student?>> UpdateStudent([FromRoute] int id, Student Student)
        {
            try
            {
                if (id == null)
                    return BadRequest("Student ID mismatch");

                var StudentToUpdate = await studentService.GetStudent(id);

                if (StudentToUpdate == null)
                    return NotFound($"Student with Id = {id} not found");

                return await studentService.UpdateStudent(id, Student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }


        [HttpDelete]
        [Route("DeleteStudent/{id:int}")]
        public async Task<ActionResult<Student?>> DeleteStudent([FromRoute] int id)
        {
            try
            {
                var StudentToDelete = await studentService.GetStudent(id);

                if (StudentToDelete == null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }
                return await studentService.DeleteStudent(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

    }
}
