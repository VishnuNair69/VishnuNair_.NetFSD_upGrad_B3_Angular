using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Relationships.Models
{
   public  class Employee
    {
        public int Id {get;set;}
        public String  Empname {get;set;}
        public string  Job {get;set;}
        public long Salary {get;set;}
        [ForeignKey("Deptid")]
        public int Deptid{get;set;}
        public Dept  Dept{get;set;}
    }
}