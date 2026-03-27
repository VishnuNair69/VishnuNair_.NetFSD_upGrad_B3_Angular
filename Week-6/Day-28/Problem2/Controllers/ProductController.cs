using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {
        // Static data (for now)
        private List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Price = 50000, Category = "Electronics" },
            new Product { Id = 2, Name = "Mobile", Price = 20000, Category = "Electronics" },
            new Product { Id = 3, Name = "Shirt", Price = 1500, Category = "Clothing" },
            new Product { Id = 4, Name = "Pants", Price = 1500, Category = "Clothing" }
        };

       
        public IActionResult Index()
        {
            return View(products);
        }

       
         
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
    }
}