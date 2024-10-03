using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Web_API.Entity
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string  UserName { get; set; }
   
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
       
    }
}
