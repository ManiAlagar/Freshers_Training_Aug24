using BookstoreApplication.Models;
using System.ComponentModel;
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
        public DateTime Ordered_Date { get; set; }
        [ForeignKey("Status")]
        [DefaultValue(1)]
        public int StatusId { get; set; }
        public decimal Discount { get; set; }
        public decimal Shipping { get; set; }  
        public decimal TotalAmount { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public string? Username { get; set; }
        [NotMapped]
        public int? Quantity { get; set; }
        [NotMapped]
        public string? Title { get; set; }
        [NotMapped]
        public decimal? Total { get; set; }
        [NotMapped]
        public string? status { get; set; }

    }
}
