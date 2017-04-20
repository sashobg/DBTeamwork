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
    public class StudioController : Controller
    {
       

        // GET: Studio
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Studio/List
        public ActionResult List()
        {
            using (var database = new MoviesDbContext())
            {
                var studios = database.Studios
                    .ToList();
                return View(studios);
            }
        }


        // GET: Studio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Studio/Create
        [HttpPost]
        public ActionResult Create(Studio studio)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    database.Studios.Add(studio);
                    database.SaveChanges();
                    TempData["Success"] = "Студиото е създадено успешно.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Danger"] = "Некоректни данни. Моля, опитайте отново.";
            return View(studio);
        }

        // GET: Studio/edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var studio = database.Studios
                    .FirstOrDefault(a => a.Id == id);

                if (studio == null)
                {
                    return HttpNotFound();
                }

                return View(studio);
            }
        }

        // GET: Studio/Edit
        [HttpPost]
        public ActionResult Edit(Studio studio)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    database.Entry(studio).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();
                    TempData["Success"] = "Студиото е редактирано успешно.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Danger"] = "Некоректни данни. Моля, опитайте отново.";
            return View(studio);
        }

        // GET: Studio/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var studio = database.Studios
                    .FirstOrDefault(s => s.Id == id);

                if (studio == null)
                {
                    return HttpNotFound();
                }

                return View(studio);
            }
        }

        // POST: Studio/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            using (var database = new MoviesDbContext())
            {
                var studio = database.Studios
                    .FirstOrDefault(a => a.Id == id);
                var movies = database.Movies.Where(m => m.StudioId == studio.Id).ToList();
                foreach (var movie in movies)
                {
                   
                    string fullPathPrimary = Request.MapPath("~/Content/UploadedImages/" + movie.Image);
                    if (System.IO.File.Exists(fullPathPrimary))
                    {
                        System.IO.File.Delete(fullPathPrimary);
                    }
                    database.Movies.Remove(movie);
                }


                database.Studios.Remove(studio);
                database.SaveChanges();
                TempData["Success"] = "Премахнат успешно.";
                return RedirectToAction("Index");

            }


        }
    }
}