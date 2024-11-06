
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if (TempData["success"] != null)
            {
                ViewBag.regSuccess = "Registered successfully";
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Register()
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
        public async Task<IActionResult> Register([Bind] User user)
        {
            
                if (ModelState.IsValid)
                {
                    
                    int res = await _service.Register(user);
                    if (res == 1)
                    {
                        TempData["success"] = "registered successfully";
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        ViewBag.registerUser = "User already exists";
                        var roles = await service.GetAllRoles();
                            var result = roles.Where(u => u.RoleName != "Admin").Select(u => new SelectListItem
                            {
                                Value = u.RoleId.ToString(),
                                Text = u.RoleName
                            });
                            ViewBag.role = result;
                            return View(user);
                    }
                  
                }


            else
            {
                ViewBag.register = "Registration failed";
                var roles = await service.GetAllRoles();
                var result = roles.Where(u => u.RoleName != "Admin").Select(u => new SelectListItem
                {
                    Value = u.RoleId.ToString(),
                    Text = u.RoleName
                });
                ViewBag.role = result;
                return View(user);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind] LoginModel user)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Login successfully";
                    var token = await _service.Login(user);
                    var res = JObject.Parse(token);
                    HttpContext.Session.SetString("token", res["token"].ToString());
                    HttpContext.Session.SetString("roleId", res["roleId"].ToString());
                    HttpContext.Session.SetString("userId", res["userId"].ToString());
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    ViewBag.login = "Enter valid credentials";
                    return View();
                }

            }
            catch
            {
                ViewBag.login = "Enter valid credentials";
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

        [HttpGet]
        [Route("User/Profile")]
        public async Task<IActionResult> Profile(int id)
        {
            var userId= HttpContext.Session.GetString("userId");
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized", "Shared");
            }
            var user = await _service.GetUserById(Convert.ToInt32(userId), token);
            if (user == null)
            {
                TempData["failure"] = "Error occurred";
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(int id, [Bind] User user)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (Convert.ToInt32(userId) > 0)
            {
                TempData["view"] = "update";
                var token = HttpContext.Session.GetString("token");
                if (token == null)
                {
                    return View("Unauthorized", "Shared");
                }
                var res=await _service.UpdateUser(Convert.ToInt32(userId), user, token);
                if (res == 1)
                {
                    TempData["success"] = "Updated successfully";
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    TempData["failure"] = " Already exists";
                }

            }
            return RedirectToAction("Profile", "User");

        }

    }
}

                       