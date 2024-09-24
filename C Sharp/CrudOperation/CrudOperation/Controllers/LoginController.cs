using Microsoft.AspNetCore.Mvc;
using CrudOperation.Models;

namespace CrudOperation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;


        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind] Login login)
        {
            LoginAccessLayer objemployee = new LoginAccessLayer(_configuration);
            if (ModelState.IsValid)
            {
                string flag = objemployee.Check(login);

               if (flag == "SUCCESS")
               {
                    TempData["Toastr"] = "Login Success";
                    return RedirectToAction("EmployeeDetails", "Employee", null);
               }
                ViewBag.msg = "ERROR";
            }
            return View(login);
        }
    }
}
