using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_MVC.Controllers
{
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
