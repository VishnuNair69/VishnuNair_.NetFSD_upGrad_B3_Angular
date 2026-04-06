using Microsoft.AspNetCore.Mvc;

namespace newapp.Controllers;

[Route("feedback")]
public class FeedbackController : Controller
{
    [Route("form")]
    [HttpGet]
    public IActionResult Form()
    {
        return View();
    }
    [Route("form")]
    [HttpPost]
    public IActionResult Form(string name, string comments, int rating)
    {
        if (rating >= 4)
        {
            ViewData["Message"] = "Thank You for your feedback!";
        }
        else
        {
            ViewData["Message"] = "We will improve based on your feedback.";
        }

        ViewData["User"] = name;

        return View();
    }
}