using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreMVC.Models
{
    public class Cart
    {
        public int CartItemId { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("Book")]
        public int? BookId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }
        public decimal? discount { get; set; }
        public decimal? Shipping { get; set; }

        [NotMapped]
        public int? Stock { get; set; }
        [NotMapped]
        public string Title { get; set; }
        [NotMapped]
        public decimal Price { get; set; }

       
    }
   
}
