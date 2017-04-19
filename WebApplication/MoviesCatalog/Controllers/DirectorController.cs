using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    public class DirectorController : Controller
    {
        // GET: Director
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Director/List
        public ActionResult List()
        {
            using (var database = new MoviesDbContext())
            {
                var directors = database.Directors
                    .ToList();
                return View(directors);
            }
        }


        // GET: Director/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Director/Create
        [HttpPost]
        public ActionResult Create(Director director)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    database.Directors.Add(director);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(director);
        }

        // GET: Director/edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var director = database.Directors
                    .FirstOrDefault(d => d.Id == id);

                if (director == null)
                {
                    return HttpNotFound();
                }

                return View(director);
            }
        }

        // GET: Director/Edit
        [HttpPost]
        public ActionResult Edit(Director director)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    database.Entry(director).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(director);
        }

        // GET: Director/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var director = database.Directors
                    .FirstOrDefault(d => d.Id == id);

                if (director == null)
                {
                    return HttpNotFound();
                }

                return View(director);
            }
        }

        // POST: Director/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            using (var database = new MoviesDbContext())
            {
                var director = database.Directors
                    .FirstOrDefault(d => d.Id == id);


                database.Directors.Remove(director);
                database.SaveChanges();
                TempData["Success"] = "Премахнат успешно.";
                return RedirectToAction("Index");

            }


        }
    }
}