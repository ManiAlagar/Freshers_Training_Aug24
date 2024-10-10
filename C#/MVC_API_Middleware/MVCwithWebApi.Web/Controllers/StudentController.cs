using DBFirstApproachCRUD.Models;
using DBFirstApproachCRUD.Repository.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.VisualStudio.Services.ClientNotification;
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
            var token= HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized");
            }
            var students = await _service.GetAll(token);
            return View(students);
        }

        [HttpPost, ActionName("DeleteStudent")]
        public async Task<bool> DeleteStudent(int id)
        {

            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            await _service.DeleteStudent(id,token);
            return true;
        }

        [HttpGet]
        public IActionResult Display()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized");
            }
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Display([Bind] Student student)
        {
            throw new Exception();
            TempData["success"] = "Created successfully";
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized");
            }
            await _service.AddStudent(student,token);

            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        [Route("Student/Display/{id}")]
        public async Task<IActionResult> Display(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized");
            }
            var student = await _service.GetbyId(id,token);
            if(student == null)
            {
                TempData["failure"] = "Error occurred";
            }
            return View(student);
        }

        [HttpPost]
        [Route("Student/Display/{id:int}")]
        public async Task<IActionResult> Display(int id, [Bind] Student student)
        {
              if (student.StudentId > 0)
                {
                    TempData["success"] = "Updated successfully";
                    TempData["view"] = "update";
                    var token = HttpContext.Session.GetString("token");
                    if(token == null)
                    {
                        return View("Unauthorized");
                        //throw new NotAuthorizedException("Bearer");
                    }
                    await _service.UpdateStudent(id, student, token);

                }
                else
                {
                    var token = HttpContext.Session.GetString("token");
                    if (token == null)
                    {
                        return View("Unauthorized");
                    }
                    await _service.AddStudent(student, token);
                }
                return RedirectToAction("Index", "Student");
            
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind] Student student)
        {
            try
            {
                TempData["success"] = "Login successfully";
                var token = await _service.Login(student);
                HttpContext.Session.SetString("token", token);
                return RedirectToAction("Index", "Student");
            }
            catch
            {
                TempData["failure"] = "Error occurred";
                return View();
            }
            
        }
        [HttpGet]
        public IActionResult Logout()
        {
            TempData["success"] = "logged out Successfully";
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Student");
        }

    }
}
