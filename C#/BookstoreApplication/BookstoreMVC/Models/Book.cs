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
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter valid Price amount")]
        [Range(0.1, 10000)]
        public decimal Price { get; set; }
        [RegularExpression(@"(\s*[0-9]{0,6})", ErrorMessage = "Enter valid count")]
        public int Stock { get; set; }
        public bool? IsPublish { get; set; } = false;
        public DateTime? PublishedDate { get; set; } = null;
        [NotMapped]
        public string? Username { get; set; }
        [NotMapped]
        public bool isAddedToCart { get; set; }
    }
}
