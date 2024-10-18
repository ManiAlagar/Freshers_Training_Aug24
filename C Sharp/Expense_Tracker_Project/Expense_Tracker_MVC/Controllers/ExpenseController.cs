using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Implement;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expense_Tracker_MVC.Controllers
{
    public class ExpenseController : Controller
    {

        private readonly IExpenseService expenseService;
        private readonly ICategoryService categoryService;
        public ExpenseController(IExpenseService expenseService, ICategoryService categoryService)
        {
            this.expenseService = expenseService;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }
            var entity = await expenseService.Get();

            return View(entity);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            //List in select area
            var Res = await categoryService.Get();

            var Result = Res.ToList().Select(d => new SelectListItem()
            {
                Value = d.CategoryID.ToString(),
                Text = d.CategoryName
            });
            ViewBag.CategoryList = Result;


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
                var entity = await expenseService.GetByID(id);
                entity.CategoryName = entity.CategoryID.ToString();
                return View(entity);
            }
        }
    }
}
