using System.ComponentModel.DataAnnotations.Schema;
namespace ContactManagementSystem.Models{
public class ContactInfo
{

 public int id {set;get;}	
 
public String FirstName {set;get;}	
public String LastName {set;get;}	
public string EmailId	{set;get;}
public long MobileNo {set;get;}
public String Designation	{set;get;}
[ForeignKey("Companyid")]
public int Companyid{get;set;}
[ForeignKey("Departmentid")]
public int Departmentid{get;set;}

public Company Company{get;set;}

public Department Department{get;set;}
}
}