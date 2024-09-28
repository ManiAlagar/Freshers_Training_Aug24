using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUD_MVC.Models;

namespace CRUD_MVC.Controllers
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
            return RedirectToAction("Index","Common");
        }


        [HttpGet]
        public IActionResult Manage()
        {
            TempData["edit"] = "edit";
            DepartmentCrud obj = new DepartmentCrud(configuration);
            var lstdept = obj.GetAllDepartments().ToList().Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentId.ToString() }); //linq-- any, all, where, exists
            ViewBag.list = lstdept;
            return View();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Manage(int id)
        {
            DepartmentCrud obj = new DepartmentCrud(configuration);
            var lstdept = obj.GetAllDepartments().ToList().Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentId.ToString() }); ;
            ViewBag.list = lstdept;
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
                var res = objemployee.AddEmployee(employee);
                if (res == "failure")
                {
                    TempData["success"] = "Created successfully";
                    return RedirectToAction("Index", "Common");
                    
                }
                else
                {
                    TempData["failure"] = "Employee Already exists";
                }
               

            }
            return View(employee);
        }


        [HttpPost]
        [Route("{id:int}")]
        public IActionResult Manage(int id,[Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Updated successfully";
                EmployeeCrud objemployee = new EmployeeCrud(configuration);
                if (employee.EmployeeID > 0)
                {
                    
                    objemployee.UpdateEmployee(employee);
                }
                else
                {
                    objemployee.AddEmployee(employee);
                }
                return RedirectToAction("Index", "Common");
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