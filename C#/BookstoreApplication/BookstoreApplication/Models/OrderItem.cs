using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreApplication.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int? UserId { get; set; }

    }
}
