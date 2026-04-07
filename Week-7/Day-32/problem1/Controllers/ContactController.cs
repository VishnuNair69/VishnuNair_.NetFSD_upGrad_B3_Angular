using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EXAPM.Models;
using EXAPM.Services;
namespace EXAPM.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService=contactService;
        }
        public IActionResult ShowContacts()
        {
            var contacts=_contactService.GetAllContacts();
            return View(contacts);
        }
        [HttpGet]
        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(ContactInfo contact)
        {
            if(ModelState.IsValid){
            _contactService.AddContact(contact);
            return RedirectToAction("ShowContacts");
            }
            else
            {
                return View();
            }

        }
        public IActionResult GetContactById(int id)
        {
            var contact=_contactService.GetContactById(id);
            return View(contact);
        }
    }
}