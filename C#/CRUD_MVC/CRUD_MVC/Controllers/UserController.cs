using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using CRUD_MVC.Models;

namespace CRUD_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;

       
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]//default get method when we try to access url.
        public async Task<IActionResult> Logout()
        {
            TempData["success"] = "logged out Successfully";
            await HttpContext.SignOutAsync();//SignOutAsync is Extension method for SignOut    
            return RedirectToAction("Login", "User");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login( [Bind] User user)
        {
            
            if (ModelState.IsValid)
            {
                UserModel obj = new UserModel(configuration);
                string res= obj.LoginCheck(user);

                if (res == "success")
                {

                    TempData["success"] = "logged in Successfully";
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName)
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(claimsIdentity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Common");
                } 
                else {
                    ViewBag.msg = "Error";
                    ModelState.Clear();
                    return View();
                    
                }

            }
            
            return View();
        }
    }
}
