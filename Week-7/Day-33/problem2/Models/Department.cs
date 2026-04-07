namespace ContactManagementSystem.Models
{
    public class Department
    {
        public int Departmentid{get;set;}
         public String Departmentname{get;set;}
         public List<ContactInfo> ContactInfos{get;set;}
    }
}