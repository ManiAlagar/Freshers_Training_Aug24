using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
    
        public string DepartmentName { get; set; }
    }
}
