namespace ContactManagementSystem.Models
{
    public class Company
    {
        public int Companyid{get;set;}
        public String Companyname{get;set;}
        public List<ContactInfo> ContactInfos{get;set;}
    }
}