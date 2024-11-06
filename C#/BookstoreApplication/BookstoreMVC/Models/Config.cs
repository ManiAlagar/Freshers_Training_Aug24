using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreMVC.Models
{
    public class Config
    {
        public int Id { get; set; }
        public string? ConfigKey { get; set; }
        [Range(0, 10000)]
        public int ConfigValue { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
    }
}
