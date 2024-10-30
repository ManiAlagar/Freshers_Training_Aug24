using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreApplication.Models
{
    [Table("Config")]
    public class Config
    {
        [Key]
        public int Id { get; set; }
        public string ConfigKey { get; set; }
        public int ConfigValue { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; } 


    }
}
