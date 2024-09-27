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

        //Select operation
        //public IActionResult Index()

        //{
        //    if (TempData["Toastr"] == null)
        //    {
        //        TempData["Toastr"] = "Nothing";
        //    }
        //    DepartmentAccess objDepartment = new DepartmentAccess(_configuration);

        //    return View(objDepartment.DepartmentDetails());
        //}



        //For Create and Update operation
        [HttpGet]
        public IActionResult DepartmentEdit(int? id)
        {
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
            TempData["Toastr"] = "Updated Successful";

            if (id != null)
            {
                objDepartment.UpdateDepartment(department);
            }
            else
            {
                objDepartment.AddDepartment(department);
                TempData["Toastr"] = "Created Successful";
            }
            return RedirectToAction("CommonModel", "Common");
        }


        //Delete Opeartion
        [HttpPost, ActionName("Delete")]
        public bool DeleteConfirmed(int? id)
        {

            DepartmentAccess objDepartment = new DepartmentAccess(_configuration);
            objDepartment.DeleteDepartment(id);
            return true;

        }

    }
}
