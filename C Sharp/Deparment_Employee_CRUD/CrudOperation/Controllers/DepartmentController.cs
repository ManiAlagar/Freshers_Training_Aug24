using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CrudOperation.Models;
using Microsoft.AspNetCore.Authorization;

namespace CrudOperation.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //For Create and Update operation
        [HttpGet]
        public IActionResult DepartmentEdit(int? id)
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }
            if (id == null)
            {
                TempData["AddOrEdit"] = "Create New Department";
                return View();
            }
            else
            {
                TempData["AddOrEdit"] = "Edit Department";
                DepartmentAccess objDepartment= new DepartmentAccess(_configuration);
                if (id == null)
                {
                    return NotFound();
                }
                Department department = objDepartment.GetDepartmentData(id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DepartmentEdit(int? id, [Bind] Department department)
        {
            DepartmentAccess objDepartment = new DepartmentAccess(_configuration);
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }

            if (id != null)
            {
                string status = objDepartment.UpdateDepartment(department);
                TempData["Toastr"] = "Updated Successful";
                if (status == "Failure")
                {
                    TempData["Toastr"] = "Department already exists";
                    return RedirectToAction("DepartmentEdit","Department");
                }
            }
            else
            {
                string status =objDepartment.AddDepartment(department);
                TempData["Toastr"] = "Created Successful";
                if (status == "Failure")
                {
                    TempData["Toastr"] = "Department already exists";
                    return RedirectToAction("DepartmentEdit", "Department");
                }
             }
            return RedirectToAction("CommonModel", "Common");
        }


        //Delete Opeartion
        [HttpPost, ActionName("Delete")]
        public bool DeleteConfirmed(int? id)
        {

            DepartmentAccess objDepartment = new DepartmentAccess(_configuration);
            string status = objDepartment.DeleteDepartment(id);

            if (status != null)
            {
                TempData["Toastr"] = "There a relation for this department in employee table";
                return true;
            }
            else
            return true;

        }

    }
}
