using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CRUD_MVC.Models;

namespace CRUD_MVC.Controllers
{
    public class CommonController : Controller
    {
        private readonly IConfiguration configuration;

        public CommonController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {

            CommonModel obj = new CommonModel();
            DepartmentCrud objRef = new DepartmentCrud(configuration);
            EmployeeCrud objemployee = new EmployeeCrud(configuration);

            obj.DepartmentList = objRef.GetAllDepartments().ToList();
            obj.EmployeeList = objemployee.GetAllEmployees();

            return View(obj);
        }
    }
}
