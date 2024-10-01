using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstApproach.Models
{
    public class Student
    {
        [Key]//primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//set identity
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public long PhoneNumber { get; set; }
        [ForeignKey("Mark")]
        public int MarkId { get; set; }
        public Mark Mark { get; set; }

    }

}
