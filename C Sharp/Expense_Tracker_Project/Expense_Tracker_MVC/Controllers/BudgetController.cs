using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Implement;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Expense_Tracker_MVC.Controllers
{
    [Authorize]
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


        [HttpGet, ActionName("Get")]
        public async Task<Budget> Get(int? id)
        {
            var entity = await budgetService.GetByID(id);
            return entity;
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["Toastr"] = "Nothing";

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

                //var a = await response.Content.ReadAsStringAsync();

                //dynamic status = JsonConvert.DeserializeObject(a);

                //return status["status"];


                Budget entity = await budgetService.GetByID(id);
                entity.CategoryName = entity.CategoryID.ToString();
                return View(entity);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind] Budget entity)
        {
            

            if (ModelState.IsValid)
            {
                TempData["Toastr"] = "Budget already Exists for the category";
                if (id != null)
                {

                    entity.Id = (int)id;

                    string status = await budgetService.Edit(entity);


                    //TempData["Toastr"] = $"Budget is Exists for the {entity.CategoryName} category";
                    if (status == "Failure")
                        TempData["Toastr"] = "Budget already Exists for the category";

                    else if (status == "Success")
                        TempData["Toastr"] = "Updated Successful";
                    else
                        TempData["Toastr"] = status;

                }
                else
                {
                    var status = await budgetService.Create(entity);


                    if (status == "Success")
                        TempData["Toastr"] = "Created Successful";
                }

            }
            else
            {
                TempData["Toastr"] = "End date should be greater than Start date ";
                TempData["AddOrEdit"] = "Create";
                return View(entity);
            }
            return RedirectToAction("Index");
        }




        [HttpPost, ActionName("Delete")]
        public async Task<bool> DeleteConfirmed(int id)
        {
           
            string status = await budgetService.Delete(id);
            TempData["Toastr"] = "Deleted Successful";
            if (status == "Failure")
            {
                TempData["Toastr"] = "Cannot delete this Bugdet it has relation in Expense details";
            }
            return true;
        }
    }
}
