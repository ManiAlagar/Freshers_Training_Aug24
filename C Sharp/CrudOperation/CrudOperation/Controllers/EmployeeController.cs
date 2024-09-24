using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CrudOperation.Models;
using Microsoft.AspNetCore.Authorization;

namespace CrudOperation.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;


        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        
        public IActionResult EmployeeDetails()
        
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }

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
            TempData["AddOrEdit"] = "Create";
            return View("Edit");
        }

        [HttpPost]
        public IActionResult Create([Bind] Employee employee)
        {
            AccessLayer objemployee = new AccessLayer(_configuration);
            //if (ModelState.IsValid)
            //{
                objemployee.AddEmployee(employee);
                TempData["Toastr"] = "Created Successful";
                return RedirectToAction("EmployeeDetails");
            ////}
            //return View(employee);
            //return RedirectToAction("EmployeeDetails");
        }


        //For Update operation

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            TempData["AddOrEdit"] = "Edit";
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
                TempData["Toastr"] = "Updated Successful";
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

    }
}
