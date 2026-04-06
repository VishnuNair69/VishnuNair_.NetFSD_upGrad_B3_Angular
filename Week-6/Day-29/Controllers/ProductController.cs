using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewMvcApp.Models;

namespace NewMvcApp.Controllers;

public class ProductController : Controller
{
    
   public static List<Product> products = new List<Product>()
            {
                
            new Product{ Id = 1, Name = "Laptop", Category = "Electronics", Price = 50000 },
            new Product{ Id = 2, Name = "Mobile", Category = "Electronics", Price = 20000 },
            new Product{ Id = 3, Name = "Shoes", Category = "Fashion", Price = 3000 }
        };
    public IActionResult Index()
    {
            return View(products);
    }
    public IActionResult Details(int Id)
    {
        var product=products.Find(p=>p.Id==Id);
        return View(product);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Product p)
    {
        if(ModelState.IsValid){
        products.Add(p);
        return RedirectToAction("Index");
        }
        else
        {
            ViewBag.Error="invalid product";
            return View();
        }
    }
    [HttpGet]
    public IActionResult Edit(int Id)
    {
        var product=products.Find(p=>p.Id==Id);
        return View(product);
    }
    [HttpPost]
    public IActionResult Edit(Product p)
    {
        if(ModelState.IsValid){
        var exproduct=products.FirstOrDefault(x=>x.Id==p.Id);
        exproduct.Name=p.Name;
        exproduct.Category=p.Category;
        exproduct.Price=p.Price;
        return RedirectToAction("Index");
        }else{
        ViewBag.Error="invalid product";}
        return View();
    }
    [HttpGet]

    public IActionResult Delete(int Id)
    {
        var product=products.Find(p=>p.Id==Id);
        return View(product);
    }
    [HttpPost]
    public IActionResult Delete(String Id)
    {
        
        var prodobj=products.Find(x=>x.Id==int.Parse(Id));
        if(products!=null){
        products.Remove(prodobj);
        return RedirectToAction("Index");
        }
        else
        {
            ViewBag.Error="Invalid userid";
            return View();
        }
        
    }
}