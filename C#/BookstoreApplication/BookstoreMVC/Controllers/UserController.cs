
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;


namespace BookstoreMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IRoleService service;


        public UserController(IUserService service, IRoleService __service)
        {
            this._service = service;
            this.service = __service;
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var roles = await service.GetAllRoles();
            var res = roles.Where(u=>u.RoleName!= "Admin").Select(u => new SelectListItem
            {
                Value = u.RoleId.ToString(),
                Text = u.RoleName
            });
            ViewBag.role = res;
            return View();
            
        }
        [HttpPost]
        public async Task<ActionResult> Register([Bind] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Registered successfully";
                    await _service.Register(user);
                    return RedirectToAction("Login", "User");
                }
                else {
                    var roles = await service.GetAllRoles();
                    var res = roles.Where(u => u.RoleName != "Admin").Select(u => new SelectListItem
                    {
                        Value = u.RoleId.ToString(),
                        Text = u.RoleName
                    });
                    ViewBag.role = res;
                    return View(user);
                }
            }
            catch
            {
                TempData["failure"] = "Error occurred";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind] LoginModel user)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    TempData["failure"] = "Enter your details ";
                    return View();
                }
                TempData["success"] = "Login successfully";
                var token = await _service.Login(user);
                var res = JObject.Parse(token);
                HttpContext.Session.SetString("token", res["token"].ToString());
                HttpContext.Session.SetString("roleId", res["roleId"].ToString());
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
            return RedirectToAction("Login", "User");
        }
    }
}
