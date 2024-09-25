using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeeController : Controller
    {
        // iconfiguration - set of key/value. used to access appsettings where i include the connection string(json format)
        private readonly IConfiguration configuration;
        
        //constructor injection
        public EmployeeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }





        //get all emp in table format -index page
        [Authorize]//requires specific authorization
        public IActionResult Index()  //IActionResult -represents the result of an action method
        {
            EmployeeCrud objemployee = new EmployeeCrud( configuration);
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = objemployee.GetAllEmployees().ToList();
           
            return View(lstEmployee);
        }

        

        


        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int? id)
        {
            EmployeeCrud objemployee = new EmployeeCrud(configuration);
            objemployee.DeleteEmployee(id);
            return RedirectToAction("Index");
        }






        [HttpGet]
        public IActionResult Manage()
        {

            return View();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Manage(int id)
        {

            EmployeeCrud objemployee = new EmployeeCrud(configuration);
            Employee employee = objemployee.GetEmployeeData(id);// GetEmployeeData returns a Employee object. otherwise we can use string ,int based on the return type of the method.

            if (employee == null)
            {
                return View();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Manage([Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                
                EmployeeCrud objemployee = new EmployeeCrud(configuration);
                objemployee.AddEmployee(employee);

            }
            return View(employee);
        }


        [HttpPost]
        [Route("{id:int}")]
        public IActionResult Manage(int id,[Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "create";
                EmployeeCrud objemployee = new EmployeeCrud(configuration);
                if (employee.EmployeeID > 0)
                {
                    objemployee.UpdateEmployee(employee);
                }
                else
                {
                    objemployee.AddEmployee(employee);
                }
                return RedirectToAction("Index");
            }
            return View(employee);
        }

    }
}


//[HttpPost]

//public IActionResult Create([Bind] Employee employee)
//{
//    if (ModelState.IsValid)
//    {
//        TempData["Message"] = "create";
//        EmployeeCrud objemployee = new EmployeeCrud(configuration);
//        objemployee.AddEmployee(employee);
//        return RedirectToAction("Index");
//    }
//    return View(employee);
//}


//[HttpPost]

//public IActionResult Edit(int id, [Bind] Employee employee)
//{
//    if (TempData["EditOrCreate"] != null)
//    {
//        return View();  
//    }



//    if (id != employee.EmployeeID)
//    {
//        return NotFound();
//    }
//    if (ModelState.IsValid)
//    {

//        EmployeeCrud objemployee = new EmployeeCrud(configuration);
//        objemployee.UpdateEmployee(employee);
//        return RedirectToAction("Index");
//    }
//    return View(employee);
//}