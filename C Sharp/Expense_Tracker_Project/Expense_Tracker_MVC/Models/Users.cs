using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker_MVC.Models
{
    public class Users
    {
        [Key]
        public int? UserID { get; set; } = 0;

     
        public string UserName { get; set; }

       
        public string? Password { get; set; }

       
        
        public string Gender { get; set; }

       
        public string Email { get; set; }


        [Required(ErrorMessage = "Mobile Number is Required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter a valid mobile Number")]
        public string MobileNumber { get; set; }
    }
}
