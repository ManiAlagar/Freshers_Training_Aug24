using System.ComponentModel.DataAnnotations;

namespace BookstoreMVC.Models
{
    public class CartRes
    {
        public IEnumerable<Cart> items { get; set; }
        public Discount discount { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}
