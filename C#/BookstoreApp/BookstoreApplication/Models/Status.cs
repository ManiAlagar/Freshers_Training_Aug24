using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreApplication.Models
{
    [Table("OrderStatus")]
    public class Status
    {
        [Key]
        public int Id { get; set; } 
        public string status { get; set; }    
    }
}
