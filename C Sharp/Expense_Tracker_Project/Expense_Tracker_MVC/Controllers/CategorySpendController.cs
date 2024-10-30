using Expense_Tracker_MVC.Service.Implement;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_MVC.Controllers
{
    public class CategorySpendController : Controller
    {
        private readonly ICategorySpendService categorySpendService;

        public CategorySpendController(ICategorySpendService categorySpendService)
        {
            this.categorySpendService = categorySpendService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }
            var entity = await categorySpendService.Get();

            return View(entity);
        }
    }
}
