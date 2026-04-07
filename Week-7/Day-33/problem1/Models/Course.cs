using System.ComponentModel.DataAnnotations;
namespace Relationships.Models
{
   public class Course
{
   
   public int Courseid { get; set; } 


    public string Coursename { get; set; }

    public List<Student> students { get; set; }
}
}