using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relationships.Models
{
    public class Student
    {
        [Key]
        public int Studentid { get; set; }

        public string Studentname { get; set; }   

        public int Courseid { get; set; }        

        [ForeignKey("Courseid")]
        public Course Course { get; set; }        
    }
}