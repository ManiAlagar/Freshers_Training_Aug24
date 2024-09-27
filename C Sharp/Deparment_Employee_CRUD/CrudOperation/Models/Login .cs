using System.ComponentModel.DataAnnotations;

namespace CrudOperation.Models
{
    public class Login
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
