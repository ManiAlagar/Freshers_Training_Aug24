using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreApplication.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CartItemId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int Quantity { get; set; } 
        public string Status { get; set; }

        [NotMapped]
        public int Stock { get; set; }
        [NotMapped]
        public string Title { get; set; }

        public decimal? Price { get; set; }

    }

    
}
