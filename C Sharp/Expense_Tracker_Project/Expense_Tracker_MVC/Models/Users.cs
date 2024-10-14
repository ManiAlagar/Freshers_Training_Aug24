using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker_MVC.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }
    }
}
