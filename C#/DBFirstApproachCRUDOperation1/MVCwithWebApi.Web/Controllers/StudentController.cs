using DBFirstApproachCRUD.Models;
using DBFirstApproachCRUD.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using MVCwithWebApi.Web.Models;
using MVCwithWebApi.Web.Services.Interfaces;
using Student = MVCwithWebApi.Web.Models.Student;
using Task = Microsoft.Exchange.WebServices.Data.Task;

namespace MVCWithWebApi.Controllers
{
   
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _service.GetAll();
            return View(students);
        }

        [HttpPost, ActionName("DeleteStudent")]
        public async Task<bool> DeleteStudent(int id)
        {
            await _service.DeleteStudent(id);
            return true;
        }

        [HttpGet]
        public IActionResult Display()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Display([Bind] Student student)
        {
            TempData["success"] = "Created successfully";
            await _service.AddStudent(student);
            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        [Route("Student/Display/{id}")]
        public async Task<IActionResult> Display(int id)
        {
           
            var student = await _service.GetbyId(id);
            if(student == null)
            {
                return View();
            }
            return View(student);
        }

        [HttpPost]
        [Route("Student/Display/{id:int}")]
        public async Task<IActionResult> Display(int id, [Bind] Student student)
        {
            if (student.StudentId > 0)
            {
                await _service.UpdateStudent(id, student);
            }
            else
            {
                await _service.AddStudent(student);
            }
            return RedirectToAction("Index", "Student");

        }
    }
}
