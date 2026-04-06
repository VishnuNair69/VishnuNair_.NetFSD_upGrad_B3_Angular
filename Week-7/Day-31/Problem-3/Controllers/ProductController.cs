using Microsoft.AspNetCore.Mvc;
namespace newapp.Controllers;

[Route("product")]
public class ProductController : Controller
{
    private static List<dynamic> productList = new List<dynamic>();

    [Route("index")]
    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Products = productList;
        return View();
    }
    [Route("index")]
    [HttpPost]
    public IActionResult Index(string name, double price, int quantity)
    {
        productList.Add(new 
        { 
            Name = name, 
            Price = price, 
            Quantity = quantity 
        });

        ViewBag.Products = productList;

        return View();
    }
}