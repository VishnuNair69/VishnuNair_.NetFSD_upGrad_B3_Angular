using Microsoft.AspNetCore.Mvc;
namespace newapp.Controllers;
[Route("student")]
public class StudentController : Controller
{
    [Route("reg")]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [Route("reg")]
    [HttpPost]
    public IActionResult Register(String name,int age,string course)
    {
        TempData["name"]=name;
        TempData["age"]=age;
        TempData["course"]=course;
        return RedirectToAction("Display");
    }
    [Route("dis")]
    [HttpGet]
    public IActionResult Display()
    {
        ViewBag.name=TempData["name"];
        ViewBag.age=TempData["age"];
        ViewBag.course=TempData["course"];
        return View();
    }
}