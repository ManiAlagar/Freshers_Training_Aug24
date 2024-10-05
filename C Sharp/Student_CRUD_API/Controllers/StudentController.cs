using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Student_CRUD_API.Entity;
using Student_CRUD_API.Services.Interface;

namespace Student_CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Students> student = await studentService.Get();
            if (student != null)
            {
                return Ok(student);
            }
            return NotFound("Record Not found");
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> Get(int id)
        {
            Students student = await studentService.GetStudent(id);
            if (student != null)
            {
                return Ok(student);
            }
          
            return NotFound("Student record not found in DB");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Students entity)
        {
            bool Status = await studentService.AddStudent(entity);

            if (Status == true)
            {
                return Ok("Created successful");
            }

            return BadRequest("Plz check the payload given there is error in it");

        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id,Students entity)
        {
            Students student = await studentService.GetStudent(id);

            if (student != null)
            {
                student.Name = entity.Name;
                student.Gender = entity.Gender;
                student.Class = entity.Class;
                student.MobileNumber = entity.MobileNumber;


                await studentService.UpdateStudent(student);
                return Ok("Updated Successful");
            }
            return NotFound("Student record not found in DB");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            Students student = await studentService.GetStudent(id);

            if (student != null)
            { 
                await studentService.DeleteStudent(student);
                return Ok("Deleted Successful");
            }
            return NotFound("Student record not found in DB");
        }

    }
}
