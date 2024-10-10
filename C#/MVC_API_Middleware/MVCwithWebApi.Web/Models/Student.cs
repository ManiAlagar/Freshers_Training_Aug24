using System.ComponentModel.DataAnnotations;

namespace MVCwithWebApi.Web.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username can only contain letters")]
        public string StudentName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(6)]
        public string Password { get; set; }
    }
}
