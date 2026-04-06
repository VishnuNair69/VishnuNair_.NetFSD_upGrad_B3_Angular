using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewMvcApp.Models;

namespace NewMvcApp.Controllers;

public class ContactController: Controller
{
    
    public static List<ContactInfo> contacts = new List<ContactInfo>
{
    new ContactInfo
    {
        ContactId = 1,
        FirstName = "Ravi",
        LastName = "Kumar",
        CompanyName = "ABC Infotech",
        EmailId = "ravi.kumar@gmail.com",
        MobileNo = 9876543210,
        Designation = "Manager"
    },

    new ContactInfo
    {
        ContactId = 2,
        FirstName = "Sita",
        LastName = "Reddy",
        CompanyName = "Tech Solutions",
        EmailId = "sita.reddy@gmail.com",
        MobileNo = 9123456780,
        Designation = "Developer"
    },

    new ContactInfo
    {
        ContactId = 3,
        FirstName = "Arjun",
        LastName = "Naidu",
        CompanyName = "Global Systems",
        EmailId = "arjun.naidu@gmail.com",
        MobileNo = 9012345678,
        Designation = "Tester"
    },

    new ContactInfo
    {
        ContactId = 1,
        FirstName = "Priya",
        LastName = "Sharma",
        CompanyName = "NextGen Pvt Ltd",
        EmailId = "priya.sharma@gmail.com",
        MobileNo = 9988776655,
        Designation = "HR"
    }

   };
    public IActionResult ShowContacts()
    {
            return View(contacts);
    }
    public IActionResult GetContactbyid(int Id)
    {
        var result=contacts.Select(item=>item);
        if(Id!=null){
        result=contacts.Where(item=>item.ContactId==Id);
        }
        return View(result.ToList());
    }
    [HttpGet]
    public IActionResult AddContact()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddContact(ContactInfo contactInfo)
    {
          if(ModelState.IsValid){
            contacts.Add(contactInfo);
            return RedirectToAction("ShowContacts");
          }
        return View();
    }
}