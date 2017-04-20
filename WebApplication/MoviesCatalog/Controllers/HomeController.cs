using Movies.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("TopMovies");
        }

        public ActionResult ListGenres()
        {
            using (var database = new MoviesDbContext())
            {
                var genres = database.Genres
                    .Include(c => c.Movies)
                    .OrderBy(c => c.Name)
                    .ToList();
               
                return View(genres); 
            }
        }

        public ActionResult TopMovies()
        {
            using (var database = new MoviesDbContext())
            {
                var movies = database.Movies.
                    OrderByDescending(m => m.Rating)
                    .Include(m => m.Genres)
                    .Take(5)
                    .ToList();

                ViewBag.Genres = database.Genres.OrderBy(g => g.Name).ToList();
                return View(movies);
            }
        }



    }
}