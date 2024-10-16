using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Expense_Tracker_MVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult>  Index()
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }
            var entity = await categoryService.Get();

            return View(entity);
        }

        [HttpGet]
        private async Task<Category> Get(int? id)
        {
            var entity = await categoryService.Get(id);
            return entity;
        }


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
                Category entity = await categoryService.Get(id);
                
                return View(entity);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind] Category entity)
        {

            TempData["Toastr"] = "Updated Successful";

            if (id != null)
            {
                entity.CategoryID = (int) id;
                              
                await categoryService.Edit(entity);
            }
            else
            {
                await categoryService.Create(entity);
                TempData["Toastr"] = "Created Successful";
            }
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public async Task<bool> DeleteConfirmed(int id)
        {
             await categoryService.Delete(id);
            return true;
        }
    }
}
