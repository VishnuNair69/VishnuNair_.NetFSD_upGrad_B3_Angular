using System.ComponentModel.DataAnnotations;

public class ContactInfo
{
[Required]
 public int ContactId {set;get;}	
 [Required]
[StringLength(10,MinimumLength =5)]
public String FirstName {set;get;}	
public String LastName {set;get;}	
[Required]
public string CompanyName {set;get;}
[EmailAddress]
public string EmailId	{set;get;}
[Required]
public long MobileNo {set;get;}
[Required]
public String Designation	{set;get;}
}