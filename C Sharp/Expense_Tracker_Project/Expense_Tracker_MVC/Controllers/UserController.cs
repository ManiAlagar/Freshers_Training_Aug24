using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
        public async Task<IActionResult> Index(int? id)
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }
            var User = await userService.Get(id);
            return View(User);
        }

        [AllowAnonymous]
        //For Create and Update operation
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {         
                TempData["Toastr"] = "Nothing";
            
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
                Users user = await userService.Get(id);
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
              

            if (id != null)
            {

                bool flag = await userService.Edit(entity);
                if (flag)
                    TempData["Toastr"] = "Updated Successfully";
                else
                    TempData["Toastr"] = "Email or MobileNumber Already exists";
                return RedirectToAction("Index", "User");

            }
            else
            {
                if(ModelState.IsValid)
                {
                    @TempData["AddOrEdit"] = "Create";
                    entity.Password = RandomString(8, true);

                    bool flag = await userService.Create(entity);
                    TempData["Toastr"] = "Email or MobileNumber Already exists";
                    if (flag)
                        TempData["Toastr"] = "Account Created Successfully";

                        return RedirectToAction("Login", "Login");
                    
                 
                }
                else
                {
                    TempData["AddOrEdit"] = "Create";
                    return View(entity);
                }
                    
                
            }
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }


    }
}
