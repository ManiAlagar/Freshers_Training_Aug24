using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService) 
        {
            this.userService = userService;
        }

        //Get By UserID
        [HttpGet]
        private async Task<Users> Get(int? id)
        {
            var User = await userService.Get(id);
            return User;
        }

        [AllowAnonymous]
        //For Create and Update operation
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["AddOrEdit"] = "Create";
                return View();
            }
            else
            {
                TempData["AddOrEdit"] = "Edit";
                if (id == null)
                {
                    return NotFound();
                }
                Users user = await Get(id);
                if (user == null)
                {
                    return NotFound();

                }
                return View(user);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind] Users entity)
        {

            TempData["Toastr"] = "Updated Successful";

            if (id != null)
            {
                //Users user = await Get(id);               
                await userService.Edit(entity);
            }
            else
            { 
                bool flag = await userService.Create(entity);
                TempData["Toastr"] = "Email or MobileNumber Already exists";
                if (flag)
                    TempData["Toastr"] = "Account Created Successfully";
                return RedirectToAction("Login","Login");
            }
            return RedirectToAction("Index","Home");
        }
    }
}
