using BookstoreApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreApplication.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal discount { get; set; }
        public decimal Shipping { get; set; }  
        public decimal TotalAmount { get; set; }
        public DateTime Ordered_Date { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
    }
}
