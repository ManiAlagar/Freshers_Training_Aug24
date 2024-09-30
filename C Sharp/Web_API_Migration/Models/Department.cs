using System.ComponentModel.DataAnnotations;

namespace Web_API_Migration.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
       
       
        public string DepartmentName { get; set; }
    }
}
