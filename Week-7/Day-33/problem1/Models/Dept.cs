using System.ComponentModel.DataAnnotations;
namespace Relationships.Models
{
   public  class Dept
    {
        public int Deptid{get;set;}
        public  String  Dname{get;set;}
        public string  Location{get;set;}
        public List<Employee> Employees{get;set;}
    }
}