using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    public class ActorController : Controller
    {
        // GET: Actor
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Actor/List
        public ActionResult List()
        {
            using (var database = new MoviesDbContext())
            {
                var actors = database.Actors
                    .ToList();
                return View(actors);
            }
        }


        // GET: Actor/Create
        public ActionResult create()
        {
            return View();
        }

        // POST: Actor/Create
        [HttpPost]
        public ActionResult Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    database.Actors.Add(actor);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(actor);
        }

        // GET: Actor/edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var actor = database.Actors
                    .FirstOrDefault(a => a.Id == id);

                if (actor == null)
                {
                    return HttpNotFound();
                }

                return View(actor);
            }
        }

        // GET: Actor/Edit
        [HttpPost]
        public ActionResult Edit(Actor actor)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    database.Entry(actor).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(actor);
        }

        // GET: Actor/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var actor = database.Actors
                    .FirstOrDefault(a => a.Id == id);

                if (actor == null)
                {
                    return HttpNotFound();
                }

                return View(actor);
            }
        }

        // POST: Category/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            using (var database = new MoviesDbContext())
            {
                var actor = database.Actors
                    .FirstOrDefault(a => a.Id == id);
                

                database.Actors.Remove(actor);
                database.SaveChanges();
                TempData["Success"] = "Премахнат успешно.";
                return RedirectToAction("Index");

            }


        }
    }
}