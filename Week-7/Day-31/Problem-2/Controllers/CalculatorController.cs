using Microsoft.AspNetCore.Mvc;
namespace newapp.Controllers;
[Route("Casio")]
public class CalculatorController : Controller
{
    [Route("Additionoperation")]
    [HttpGet]
    public IActionResult Sum()
    {
        return View();
    }
    [Route("Additionoperation")]
    [HttpPost]
    public IActionResult Sum(int num1,int num2)
    {
        int result=num1+num2;
        ViewData["sum"]=result;
        return View();
    }
}