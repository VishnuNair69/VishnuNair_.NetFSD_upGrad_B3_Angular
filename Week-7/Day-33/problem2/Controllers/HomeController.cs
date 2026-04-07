using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManagementSystem.Models;
using ContactManagementSystem.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContactManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _service;

        public HomeController(IContactService service)
        {
            _service = service;
        }

        public IActionResult ShowContacts()
        {
            var contacts = _service.GetAllContacts();
            return View(contacts);
        }

        [HttpGet]
        public IActionResult AddContact()
        {
            LoadData();
            return View();
        }

        [HttpPost]
public IActionResult AddContact(ContactInfo contact)
{
    
        _service.AddContact(contact);
        return RedirectToAction("ShowContacts");
    
    
}

        [HttpGet]
        public IActionResult EditContact(int id)
        {
            var contact = _service.GetContactById(id);

            if (contact == null) 
            {
                return NotFound();
            }

            LoadData(contact.Companyid, contact.Departmentid);
            return View(contact);
        }

        [HttpPost]
        public IActionResult EditContact(ContactInfo contact)
        {    
        _service.UpdateContact(contact);
            return RedirectToAction("ShowContacts");
        }
        [HttpGet]
         public IActionResult DeleteContact(int id)
        {
          var contact = _service.GetContactById(id);

          if (contact == null)
         {
        return NotFound();
    }

    return View(contact);
}

[HttpPost]
[ActionName("DeleteContact")]
public IActionResult DeleteConfirmed(int id)
{
    _service.DeleteContact(id);
    return RedirectToAction("ShowContacts");
}
        private void LoadData(int? companyid = null, int? departmentid = null)
        {
            ViewBag.Companies = new SelectList(
                _service.GetCompanies(),
                "Companyid",
                "Companyname",
                companyid
            );

            ViewBag.Departments = new SelectList(
                _service.GetDepartments(),
                "Departmentid",
                "Departmentname",
                departmentid
            );
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}