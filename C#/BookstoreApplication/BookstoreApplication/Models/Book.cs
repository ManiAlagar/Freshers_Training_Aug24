using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreApplication.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public int? UserId { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public int Stock {  get; set; }
        public bool? IsPublish { get; set; } = false;
        public DateTime? PublishedDate { get; set; } = null;

        [NotMapped]
        public string? Username { get; set; }
        [NotMapped]
        public bool isAddedToCart { get; set; }
    }
}
