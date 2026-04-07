using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using relationships.Models;
using Relationships.Models;

namespace relationships.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context=context;
    }
    [HttpGet]
    public IActionResult AddCourse()
{
    return View();
}

[HttpPost]
public IActionResult AddCourse(Course course)
{
    var ex=_context.Courses.FirstOrDefault(c=>c.Coursename.ToLower()==course.Coursename.ToLower());
    if(ex==null){
    _context.Courses.Add(course);
    _context.SaveChanges();
    return RedirectToAction("Students");}
        else
        {
            ViewBag.Error="the course already exist";
            return View();
        }
}
    public IActionResult Students()
    {
        var students=_context.Students.Include(c=>c.Course).ToList();
        return View(students);
    }
    [HttpGet]
    public IActionResult AddStudent()
    {
        return View();
    }
   [HttpPost]
public IActionResult AddStudent(Student student, string Coursename)
{
    var course = _context.Courses.FirstOrDefault(c => c.Coursename == Coursename);
    if (course == null)
    {
        course = new Course
        {
            Coursename = Coursename
        };

        _context.Courses.Add(course);
        _context.SaveChanges();
    }
    student.Courseid = course.Courseid;
    _context.Students.Add(student);
    _context.SaveChanges();

    return RedirectToAction("Students");
}

    public IActionResult Emps()
    {
        var emps=_context.Employeess.Include(e=>e.Dept).ToList();
        return View(emps);
    }
    public IActionResult Depts()
    {
        var dep=_context.Depts.Include(d=>d.Employees).ToList();
        return View(dep);
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
