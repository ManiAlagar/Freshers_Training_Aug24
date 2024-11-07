using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Implement;
using Expense_Tracker_MVC.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Expense_Tracker_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategorySpendService categorySpendService;
        private readonly ICategoryService categoryService;
        private readonly IExpenseService expenseService;

        public HomeController(ILogger<HomeController> logger, ICategorySpendService categorySpendService, ICategoryService categoryService, IExpenseService expenseService)
        {
            _logger = logger;
            this.categorySpendService = categorySpendService;
            this.categoryService = categoryService;
			this.expenseService = expenseService;

		}

        public async Task<IActionResult> Index()
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }

            IEnumerable<Expenses> expense = await expenseService.Get();

            var count = expense.Count();

            if (count > 0)
            {
                var data = await categorySpendService.Get();

                List<DataPoint> dataPoint = new List<DataPoint>();

                foreach (var item in data)
                {
                    dataPoint.Add(new DataPoint($"{item.CategoryName}", item.AmountSpend));
                }
                ViewBag.DataPoint = JsonConvert.SerializeObject(dataPoint);

                var monthData = await expenseService.GetMonthly();
                var categories = data.Select(x => x.CategoryName).Distinct().ToList();


                List<DataPointList> dataPointLists = new List<DataPointList>();

                foreach (var category in categories)
                {
                    DataPointList dataPointList = new();
                    dataPointList.Name = category;
                    //dataPointList.DataPoints = data.Where(a => a.CategoryName == category).Select(m => new DataPoint(m.Month_Name, m.AmountSpend)).ToList();
                    dataPointList.DataPoints = monthData.Where(a => a.CategoryName == category).GroupBy(n => new { n.Month_Name, n.CategoryName, n.AmountSpend })
                    .Select(g => new DataPoint(g.Key.Month_Name, g.Key.AmountSpend)).ToList();

                    dataPointLists.Add(dataPointList);

                }
                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPointLists, Formatting.Indented);
            }

			return View();
        }

        public async Task<IActionResult> Privacy()
        {
			//var data = await expenseService.GetMonthly();
			//var categories = data.Select(x => x.CategoryName).Distinct().ToList();
		

			//List<DataPointList> dataPointLists = new List<DataPointList>();

			//foreach (var category in categories)
			//{
			//	DataPointList dataPointList = new();
			//	dataPointList.Name = category;
			//	//dataPointList.DataPoints = data.Where(a => a.CategoryName == category).Select(m => new DataPoint(m.Month_Name, m.AmountSpend)).ToList();
			//	dataPointList.DataPoints = data.Where(a => a.CategoryName == category).GroupBy(n => new { n.Month_Name,n.CategoryName, n.AmountSpend })
			//	.Select(g => new DataPoint(g.Key.Month_Name,g.Key.AmountSpend)).ToList();

			//dataPointLists.Add(dataPointList);
				
			//}


		
			//ViewBag.DataPoints = JsonConvert.SerializeObject(dataPointLists, Formatting.Indented);


			return View();
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
