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
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }

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

            TempData["Toastr"] = "Category Already Exists";

            if (id != null)
            {
               entity.CategoryID = (int) id;
                              
               var status = await categoryService.Edit(entity);
                if (status != "Failure")
                   TempData["Toastr"] = "Updated Successfully";
            }
            else
            {
                var status =  await categoryService.Create(entity);
                if (status != "Failure") 
                 TempData["Toastr"] = "Created Successfully";
            }
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public async Task<bool> DeleteConfirmed(int id)
        {
            TempData["Toastr"] = "Deleted Successfully";
            var status = await categoryService.Delete(id);

            if(status == "Failure")
                TempData["Toastr"] = "Category has relation in Budget";
            return true;
        }
    }
}
