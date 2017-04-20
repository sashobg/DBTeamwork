using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        // GET: Genre
    
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Genre/List
        public ActionResult List()
        {
            using (var database = new MoviesDbContext())
            {
                var genres = database.Genres
                    .ToList();
                return View(genres);
            }
        }


        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    database.Genres.Add(genre);
                    database.SaveChanges();
                    TempData["Success"] = "Жанрът е създаден успешно.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Danger"] = "Некоректни данни. Моля, опитайте отново.";
            return View(genre);
        }

        // GET: Genre/edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var genre = database.Genres
                    .FirstOrDefault(a => a.Id == id);

                if (genre == null)
                {
                    return HttpNotFound();
                }

                return View(genre);
            }
        }

        // POST: Genre/Edit
        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    database.Entry(genre).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();
                    TempData["Success"] = "Жанрът е редактиран успешно.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Danger"] = "Некоректни данни. Моля, опитайте отново.";
            return View(genre);
        }

        // GET: Genre/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var genre = database.Genres
                    .FirstOrDefault(s => s.Id == id);

                if (genre == null)
                {
                    return HttpNotFound();
                }

                return View(genre);
            }
        }

        // POST: Genre/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            using (var database = new MoviesDbContext())
            {
                var genre = database.Genres
                    .FirstOrDefault(a => a.Id == id);


                database.Genres.Remove(genre);
                database.SaveChanges();
                TempData["Success"] = "Премахнат успешно.";
                return RedirectToAction("Index");

            }


        }

    }
}