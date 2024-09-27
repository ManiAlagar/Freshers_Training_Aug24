using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUD_MVC.Models;

namespace CRUD_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IConfiguration configuration;
        public DepartmentController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            DepartmentCrud obj = new DepartmentCrud(configuration);
            List<Department> lstdept = new List<Department>();
            lstdept = obj.GetAllDepartments().ToList();

            return View(lstdept );
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int? id)
        {
            DepartmentCrud obj = new DepartmentCrud(configuration);
            obj.DeleteDepartment(id);
            return RedirectToAction("Index", "Common");
        }


        [HttpGet]
        public IActionResult Display()
        {
            TempData["update"] = "update";
            return View();
        }

        
        [HttpPost]

        public IActionResult Display([Bind] Department department)
        {

            if (ModelState.IsValid)
            {
                DepartmentCrud obj = new DepartmentCrud(configuration);
                var res=obj.AddDepartment(department);
                if (res == "success")
                {
                    TempData["success"] = "Created successfully";
                    return RedirectToAction("Index", "Common");
                }
                else
                {
                    TempData["failure"] = "Already exists";
                }
                
            }
            return View(department);
        }


        [HttpGet]
        [Route("Department/Display/{id}")]
        public IActionResult Display(int id)
        {
            DepartmentCrud obj = new DepartmentCrud(configuration);
            var lstdept = obj.GetAllDepartments().ToList().Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentId.ToString() }); ;
            ViewBag.list = lstdept;
            Department department = obj.GetDepartmentData(id);

            if (department == null)
            {
                return View();
            }
            return View(department);
        }

        [HttpPost]
        [Route("Department/Display/{id:int}")]
        public IActionResult Display(int id, [Bind] Department department)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Edited successfully";
                DepartmentCrud obj = new DepartmentCrud(configuration);
                if (department.DepartmentId > 0)
                {
                    
                    obj.UpdateDepartment(department);
                }
                else
                {
                    obj.AddDepartment(department);
                }
                return RedirectToAction("Index" , "Common");
            }
            return View(department);
        }
    }
}
