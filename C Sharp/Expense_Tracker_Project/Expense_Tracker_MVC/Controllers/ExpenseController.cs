using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expense_Tracker_MVC.Controllers
{
    public class ExpenseController : Controller
    {

        private readonly IExpenseService expenseService;
        private readonly IBudgetService budgetService;
        public ExpenseController(IExpenseService expenseService, IBudgetService budgetService)
        {
            this.expenseService = expenseService;
            this.budgetService = budgetService;
        }
        public async Task<IActionResult> Index()
        { 
             TempData["Toastr"] = "Nothing";

            if (TempData["ExpenseToastr"] == null)
                TempData["ExpenseToastr"] = "Nothing";
            
            var entity = await expenseService.Get();

            return View(entity);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["Toastr"] = "Nothing";


            //List in select area
            var status = await budgetService.Get();

            if (status == null)
                return RedirectToAction("Index","Login");

            var Result = status.ToList().Select(d => new SelectListItem()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind] Expenses entity)
        {

            string status =  await expenseService.Create(entity);

            TempData["ExpenseToastr"] = status;

            return RedirectToAction("Index","CommonModel");
        }



        [HttpPost, ActionName("Delete")]
        public async Task<bool> DeleteConfirmed(int id)
        {
            await expenseService.Delete(id);
            return true;
        }
    }
}
