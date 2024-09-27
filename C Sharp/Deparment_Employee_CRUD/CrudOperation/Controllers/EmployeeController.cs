using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using CrudOperation.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        //public IActionResult CommonModel()
        //{
        //    if (TempData["Toastr"] == null)
        //    {
        //        TempData["Toastr"] = "Nothing";
        //    }

        //    CommonModel objCommonModel = new CommonModel();
        //    AccessLayer objemployee = new AccessLayer(_configuration);
        //    DepartmentAccess objDepartment = new DepartmentAccess(_configuration);

        //    objCommonModel.Employee = JsonConvert.DeserializeObject<List<Employee>>(JsonConvert.SerializeObject(objemployee.EmployeeDetails()));
        //    objCommonModel.Department = JsonConvert.DeserializeObject<List<Department>>(JsonConvert.SerializeObject(objDepartment.DepartmentDetails()));

        //    return View(objCommonModel);
        //}


        public IActionResult EmployeeDetails()

        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }
            AccessLayer objemployee = new AccessLayer(_configuration);

            return View(objemployee.EmployeeDetails());
        }

        //For Create and Update operation
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            DepartmentAccess objDepartment = new DepartmentAccess(_configuration);

            var Result = JsonConvert.DeserializeObject<List<Department>>(JsonConvert.SerializeObject(objDepartment.DepartmentDetails()))
                .Select(d => new SelectListItem()
                {
                    Value = d.DepartmentID.ToString(),
                    Text = d.DepartmentName
                });
            ViewBag.DepartmentList = Result;

            if (id == null)
            {
                TempData["AddOrEdit"] = "Create";
                return View();
            }
            else 
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] Employee employee)
        {
            AccessLayer objemployee = new AccessLayer(_configuration);
            TempData["Toastr"] = "Updated Successful";

            if (id != null)
                {
                    objemployee.UpdateEmployee(employee);
                }
                else
                {
                    objemployee.AddEmployee(employee);
                    TempData["Toastr"] = "Created Successful";
            }
                return RedirectToAction("CommonModel","Common");
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
