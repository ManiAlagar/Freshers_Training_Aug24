using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CrudOperation.Models;

namespace CrudOperation.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;


        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult EmployeeDetails()
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            string sql = "EXEC ViewTable_Employee";

            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter data = new SqlDataAdapter(sql, conn);
                data.Fill(dataTable);
            }
            return View(dataTable);
        }


        //For Create operation
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Employee employee)
        {
            AccessLayer objemployee = new AccessLayer(_configuration);
            if (ModelState.IsValid)
            {
                objemployee.AddEmployee(employee);
                return RedirectToAction("EmployeeDetails");
            }
            return View(employee);
        }

     
       //For Update operation

       [HttpGet]
        public IActionResult Edit(int? id)
        {
            AccessLayer objemployee = new AccessLayer(_configuration);
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = objemployee.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Employee employee)
        {
            AccessLayer objemployee = new AccessLayer(_configuration);
            if (id != employee.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objemployee.UpdateEmployee(employee);
                return RedirectToAction("EmployeeDetails");
            }
            return View(employee);
        }



        //Delete Opeartion
        [HttpPost, ActionName("Delete")]
        public bool DeleteConfirmed(int? id)
        {
            AccessLayer objemployee = new AccessLayer(_configuration);
            objemployee.DeleteEmployee(id);

            return true;

        }
        public ActionResult AddToCart(int id)
        {
            TempData["message"] = "Added";
            return RedirectToAction("EmployeeDetails");
        }

    }
}
