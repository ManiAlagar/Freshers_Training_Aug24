using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Implement;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Expense_Tracker_MVC.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IBudgetService budgetService;
        private readonly ICategoryService categoryService;
        public BudgetController(IBudgetService budgetService, ICategoryService categoryService)
        {
            this.budgetService = budgetService;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }
            var entity = await budgetService.Get();

            return View(entity);
        }


        [HttpGet]
        private async Task<Budget> Get(int? id)
        {
            var entity = await budgetService.GetByID(id);
            return entity;
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
                Budget entity = await budgetService.GetByID(id);

                return View(entity);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind] Budget entity)
        {

            TempData["Toastr"] = "Updated Successful";

            if (id != null)
            {
                entity.Id = (int)id;

                await budgetService.Edit(entity);
            }
            else
            {
                await budgetService.Create(entity);
                TempData["Toastr"] = "Created Successful";
            }
            return RedirectToAction("Index");
        }




        [HttpPost, ActionName("Delete")]
        public async Task<bool> DeleteConfirmed(int id)
        {
            await budgetService.Delete(id);
            return true;
        }
    }
}
