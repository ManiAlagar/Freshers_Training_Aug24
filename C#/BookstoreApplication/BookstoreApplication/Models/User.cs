using BookstoreApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace BookstoreApplication.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public long? ContactNo { get; set; }
        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        //public Role Role { get; set; }
    }
}
