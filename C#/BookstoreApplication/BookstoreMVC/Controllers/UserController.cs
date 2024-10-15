using BookstoreApplication.Repository.Interface;
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookstoreMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register([Bind] User user)
        {
            try
            {
                TempData["success"] = "Registered successfully";
                await _service.Register(user);
                return RedirectToAction("Login", "User");
            }
            catch
            {
                TempData["failure"] = "Error occurred";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind] User user)
        {
            try
            {
                TempData["success"] = "Login successfully";
                var token = await _service.Login(user);
                HttpContext.Session.SetString("token", token);
                return RedirectToAction("Index", "Book");
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
            return RedirectToAction("Register", "User");
        }
    }
}
