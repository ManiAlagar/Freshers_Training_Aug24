using CRUD_USER.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Users_CRUD.Web.Services.Interfaces;


namespace CRUD_USER.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService service;


        public LoginController(IUserService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public IActionResult Login()
        {
            return View();
        }



        //Step 3.For Authentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind] Login credential)
        {
            if (ModelState.IsValid)
            {
                var token = await service.Login(credential);

                if (token != null)
                {
                    TempData["Toastr"] = "Login Success";

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.UserData,token)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index","User");
                }
                ViewBag.msg = "ERROR";
                return View("Login");
            }
            return View("Login");
        }


        public async Task<IActionResult> LogOut()
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.UserData, String.Empty)
            };

            await HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}
