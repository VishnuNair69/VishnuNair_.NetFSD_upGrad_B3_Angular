using Microsoft.AspNetCore.Mvc;
using webapp25.Services;
using webapp25.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ContactController : Controller
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var contacts = _service.GetAllContacts();
        return View(contacts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadDropdowns();
        return View();
    }

    [HttpPost]
    public IActionResult Create(ContactInfo contact)
    {
        if(ModelState.IsValid){
        _service.AddContact(contact);
        return RedirectToAction("Index");
        }
        LoadDropdowns();
        return View(contact);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var contact = _service.GetContactById(id);
        LoadDropdowns();
        return View(contact);
    }

  [HttpPost]
public IActionResult Edit(ContactInfo contact)
{
    if (!ModelState.IsValid)
    {
        LoadDropdowns();
        return View(contact);
    }

    _service.UpdateContact(contact);
    return RedirectToAction("Index");
}
    [HttpGet]
public IActionResult Delete(int id)
{
    var contact = _service.GetContactById(id);
    return View(contact);
}

[HttpPost]
[Route("Delete")]
public IActionResult DeleteConfirmed(int contactId) 
{
    _service.DeleteContact(contactId);
    return RedirectToAction("Index");
}

    private void LoadDropdowns()
    {
        ViewBag.Companies = new SelectList(_service.GetCompanies(), "CompanyId", "CompanyName");
        ViewBag.Departments = new SelectList(_service.GetDepartments(), "DepartmentId", "DepartmentName");
    }
}