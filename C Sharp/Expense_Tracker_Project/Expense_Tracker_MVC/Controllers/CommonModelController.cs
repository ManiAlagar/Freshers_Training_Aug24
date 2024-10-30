using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Expense_Tracker_MVC.Controllers
{
    public class CommonModelController : Controller
    {
        private readonly IExpenseService expenseService;
        private readonly ICategoryService categoryService;
        private readonly ICategorySpendService categorySpendService;

        public CommonModelController(IExpenseService expenseService, ICategoryService categoryService, ICategorySpendService categorySpendService)
        {
            this.expenseService = expenseService;
            this.categoryService = categoryService;
            this.categorySpendService = categorySpendService;
        }


        public async Task<IActionResult>  Index()
        {
            TempData["Toastr"] = "Nothing";
            if (TempData["ExpenseToastr"] == null )
            {
                TempData["ExpenseToastr"] = "Nothing";
            }
            CommonModel objCommonModel = new CommonModel();
           
            objCommonModel.Expenses = await expenseService.Get();
            objCommonModel.CategorySpend = await categorySpendService.Get();




            return View(objCommonModel);
           
        }
    }





}
