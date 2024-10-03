using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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
        [StringLength(20)]
        public string  UserName { get; set; }
   
        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
       
    }
}
