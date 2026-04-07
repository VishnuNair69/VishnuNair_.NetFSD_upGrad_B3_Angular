using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using problem2.Models;

namespace problem2.Controllers
{
    public class MovieController : Controller
    {
       private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }
        public  IActionResult ToList()
        {
            return View( _context.Movies.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if(ModelState.IsValid){
            _context.Add(movie);
            _context.SaveChanges();
            return RedirectToAction("ToList");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var res = _context.Movies.Find(id);
            return View(res);
        }
         [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);     
                _context.SaveChanges();             
                return RedirectToAction("ToList");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var res = _context.Movies.Find(id);
            return View(res);
        }
         [HttpPost]
         [Route("Delete")]
        public IActionResult Remove(int id )
        {
            if (ModelState.IsValid)
            {
                var res=_context.Movies.Find(id);
                _context.Movies.Remove(res);     
                _context.SaveChanges();             
                return RedirectToAction("ToList");
            }
            else
            {
                return View();
            }
        }
    }
}