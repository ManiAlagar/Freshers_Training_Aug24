using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
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

        //Login View Page
        public IActionResult LogIn()
        {
            return View();
        }

        //Step 3.For Authentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind] Login login)
        {
            LoginAccessLayer objemployee = new LoginAccessLayer(_configuration);
            if (ModelState.IsValid)
            {
                string flag = objemployee.Check(login);
                if (flag == "SUCCESS")
                {
                    TempData["Toastr"] = "Login Success";

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login.UserName)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("CommonModel", "Common");
                }
                ViewBag.msg = "ERROR";
            }
            return View("Login");
        }

        public async Task<IActionResult> LogOut()
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, String.Empty)
            };

            await HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }

    }
}
