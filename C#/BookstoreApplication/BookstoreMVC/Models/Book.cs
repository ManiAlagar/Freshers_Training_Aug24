using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreMVC.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }
        [Range(0, 100)]
        public int Stock { get; set; }
        public bool? IsPublish { get; set; } = false;
        public DateTime? PublishedDate { get; set; } = null;
        [NotMapped]
        public string? Username { get; set; }
        [NotMapped]
        public bool isAddedToCart { get; set; }
    }
}
