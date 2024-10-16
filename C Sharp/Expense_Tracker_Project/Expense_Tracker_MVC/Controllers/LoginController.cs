using Expense_Tracker_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Expense_Tracker_MVC.Service.Interface;
using System.IdentityModel.Tokens.Jwt;


namespace Expense_Tracker_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService service;
        private readonly IHttpContextAccessor httpContextAccessor;


        public LoginController(IUserService service, IHttpContextAccessor httpContextAccessor)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            if (TempData["Toastr"] == null) 
            {
                TempData["Toastr"] = "Nothing";
            }
            return View();
        }

        //Step 3.For Authentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind] Login credential)
        {
            TempData["Toastr"] = "Nothing";
            if (ModelState.IsValid)
            {
                var token = await service.Login(credential);
               

                if (token != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var UserID = tokenS.Claims.First(claim => claim.Type == "UserID").Value;

                    TempData["Toastr"] = "Login Success";

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.UserData,token),
                    new Claim(ClaimTypes.Sid,UserID)
                };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");
                   


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
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
            TempData["Toastr"] = "Logged out";
            return RedirectToAction("login");
        }
    }
}
