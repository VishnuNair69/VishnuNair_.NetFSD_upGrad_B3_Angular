using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp36.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string StudentName { get; set; }   
          [ForeignKey("CourseId")]
        public int CourseId { get; set; }        

        
        public Course Course { get; set; }        
    }
}