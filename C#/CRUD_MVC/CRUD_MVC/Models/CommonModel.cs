using System;

namespace CRUD_MVC.Models
{
    public class CommonModel
    {
        public IEnumerable<Employee> EmployeeList { get; set; }
        public IEnumerable<Department> DepartmentList { get; set; }
    }
}
