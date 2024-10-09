using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DBFirstApproachCRUD.Models
{
    [Table("StudentProfile")]
    public class Student
    {
        [Key]
        
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
    }
}
