using CrudOperation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrudOperation.Controllers
{
    [Authorize]
    public class CommonController : Controller
    {
        private readonly IConfiguration _configuration;


        public CommonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        public IActionResult CommonModel()
        {
            if (TempData["Toastr"] == null)
            {
                TempData["Toastr"] = "Nothing";
            }

            CommonModel objCommonModel = new CommonModel();
            AccessLayer objemployee = new AccessLayer(_configuration);
            DepartmentAccess objDepartment = new DepartmentAccess(_configuration);

            objCommonModel.Employee = objemployee.EmployeeDetails().ToList();
            objCommonModel.Department = objDepartment.DepartmentDetails().ToList();

            return View(objCommonModel);
        }
    }
}
