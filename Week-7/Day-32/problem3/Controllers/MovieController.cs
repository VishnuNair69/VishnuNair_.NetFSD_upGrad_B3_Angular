using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using problem2.Models;
using problem2.Services;

namespace problem2.Controllers
{
    public class MovieController : Controller
    {
       private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }
        public  IActionResult ToList()
        {
            return View( _service.GetMovies());
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
            _service.CreateMovie(movie);
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
            var res = _service.GetById(id);
            return View(res);
        }
         [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _service.EditMovie(movie);            
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
            var res = _service.GetById(id);
            return View(res);
        }
         [HttpPost]
         [Route("Delete")]
        public IActionResult Remove(int id )
        {
                _service.DeleteMovie(id);
                return RedirectToAction("ToList");
        
            
        }
    }
}