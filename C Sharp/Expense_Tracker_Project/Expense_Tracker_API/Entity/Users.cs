using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker_API.Entity
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is Required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string MobileNumber { get; set; }

       // public string MobileNumber { get; set; }
    }
}
