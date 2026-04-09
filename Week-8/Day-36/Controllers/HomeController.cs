using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webapp36.Models;
using webapp36.repositories;

namespace webapp36.Controllers;

public class HomeController : Controller
{
    private readonly IRepository _repo;

    public HomeController(IRepository repo)
    {
        _repo=repo;
    }
    public IActionResult Students()
    {
        var s= _repo.GetStudentsWithCourses();
        return View(s);
    }
    public IActionResult Courses()
    {
        var c=_repo.GetCoursesWithStudents();
        return View(c);
    }
    public IActionResult Index()
    {
        return View();
    }
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
