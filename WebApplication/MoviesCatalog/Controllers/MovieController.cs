using Movies.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Movies.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Movie/List
        public ActionResult List(int? genreId, string search)
        {
           
            using (var database = new MoviesDbContext())
            {
                var movies = database.Movies
                    .Include(a => a.Genres)
                    .Include(a => a.Director)
                    .Include(a => a.Studio)
                    .Include(a => a.Actors)
                     .ToList();
                if (genreId != null)
                {
                    ViewBag.categoryId = genreId;
                    movies = movies.Where(a => a.Genres.Any(g => g.Id == genreId)).ToList();
                }

                if (!String.IsNullOrEmpty(search))
                {
                    movies = movies.Where(s => s.Title.Contains(search) ||
                    s.Actors.Any(a=>(a.FirstName + " " + a.LastName).Contains(search)) || 
                    s.Studio.Name.Contains(search))
                        .ToList();
                }
                ViewBag.CurrentFilter = search;
                return View(movies);
            }
            
        }

        // GET: Movie/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                // Get the movie from db
                var movie = database.Movies
                    .Where(a => a.Id == id)
                    .Include(a => a.Genres)
                    .Include(a => a.Director)
                    .Include(a => a.Studio)
                    .Include(a => a.Actors)
                    .First();

                if (movie == null)
                {
                    return HttpNotFound();
                }

                return View(movie);
            }
        }


        // GET: Movie/Create
        [Authorize]
        public ActionResult Create()
        {
            using (var database = new MoviesDbContext())
            {
                var movieViewModel = new MovieViewModel();

                var allGenresList = database.Genres.OrderBy(g => g.Name).ToList();

                movieViewModel.AllGenres = allGenresList.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                });


                var allActorsList = database.Actors.OrderBy(d => d.FirstName).ThenBy(d => d.LastName).ToList();

                movieViewModel.AllActors = allActorsList.Select(o => new SelectListItem
                {
                    Text = o.FirstName + " " + o.LastName,
                    Value = o.Id.ToString()
                });

                var allDirectors = database.Directors.OrderBy(d => d.FirstName).ThenBy(d => d.LastName).ToList();

                movieViewModel.AllDirectors = allDirectors.Select(o => new SelectListItem
                {
                    Text = o.FirstName + " " + o.LastName,
                    Value = o.Id.ToString()
                });

                var allStudios = database.Studios.OrderBy(s => s.Name).ToList();

                movieViewModel.AllStudios = allStudios.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                });

                return View(movieViewModel);
            }
        }

        // POST: Movie/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(MovieViewModel model, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    var movie = new Movie
                    {
                        Title = model.Title,
                        Plot = model.Plot,
                        Rating = model.Rating,
                        ReleaseDate = model.ReleaseDate,
                        Country = model.Country,
                        DirectorId = model.DirectorId,
                        StudioId = model.StudioId
                    };


                    foreach (var genreId in model.SelectedGenres)
                    {
                            var genre = new Genre { Id = genreId };
                            database.Genres.Attach(genre); 
                            movie.Genres.Add(genre);
                    }

                    foreach (var actorId in model.SelectedActors)
                    {
                            var actor = new Actor { Id = actorId };
                            database.Actors.Attach(actor); 
                            movie.Actors.Add(actor);
                    }



                    //Upload images
                    var validImageTypes = new string[]
                    {
                        "image/gif",
                        "image/jpeg",
                        "image/pjpeg",
                        "image/png"
                    };

                    //upload Primary image



                    if (picture != null && picture.ContentLength > 0)
                        if (validImageTypes.Contains(picture.ContentType))
                        {
                            var id = Guid.NewGuid().ToString();
                            var fileName = id + Path.GetExtension(picture.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/UploadedImages"), fileName);
                            picture.SaveAs(path);

                            movie.Image = fileName;
                            

                        }
                        else
                        {
                            TempData["Warning"] = "Позволените формати за снимка са .gif, .jpeg и .png. Филмът е качен без снимка.";
                            return RedirectToAction("List");
                        }

                    database.Movies.Add(movie);
                    database.SaveChanges();




                }
                TempData["Success"] = "Успешно добавихте обява. Обявата Ви ще бъде видима, след като е одобрена.";
                return RedirectToAction("List");
            }
            TempData["Danger"] = "Некоректни данни, моля опитайте отново.";
            return RedirectToAction("List");
        }


        // GET: Movie/Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
                var movieViewModel = new MovieViewModel();

                var movie = database.Movies
                    .First(a => a.Id == id);


                // Check if Movie exists
                if (movie == null)
                {
                    return HttpNotFound();
                }

                movieViewModel.Id = movie.Id;
                movieViewModel.Title = movie.Title;
                movieViewModel.Plot = movie.Plot;
                movieViewModel.Rating = movie.Rating;
                movieViewModel.Country = movie.Country;
                movieViewModel.ReleaseDate = movie.ReleaseDate;
                movieViewModel.DirectorId = movie.DirectorId;
                movieViewModel.StudioId = movie.StudioId;
                movieViewModel.SelectedActors = movie.Actors.Select(a => a.Id).ToList();
                movieViewModel.SelectedGenres = movie.Genres.Select(g => g.Id).ToList();


                var allGenresList = database.Genres.OrderBy(g => g.Name).ToList();

                movieViewModel.AllGenres = allGenresList.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                });


                var allActorsList = database.Actors.OrderBy(d => d.FirstName).ThenBy(d => d.LastName).ToList();

                movieViewModel.AllActors = allActorsList.Select(o => new SelectListItem
                {
                    Text = o.FirstName + " " + o.LastName,
                    Value = o.Id.ToString()
                });

                var allDirectors  = database.Directors.OrderBy(d => d.FirstName).ThenBy(d => d.LastName).ToList();

                movieViewModel.AllDirectors = allDirectors.Select(o => new SelectListItem
                {
                    Text = o.FirstName + " " + o.LastName,
                    Value = o.Id.ToString()
                });

                var allStudios = database.Studios.OrderBy(s => s.Name).ToList();

                movieViewModel.AllStudios = allStudios.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                });





                return View(movieViewModel);
            }
        }


        // POST: Movie/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(MovieViewModel model)
        {
            // Check if model state is valid
            if (ModelState.IsValid)
            {
                using (var database = new MoviesDbContext())
                {
                    // Get Movie from database
                    var movie = database.Movies
                        .FirstOrDefault(a => a.Id == model.Id);

                    if (!IsUserAuthorizedToEdit(movie))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
                    var updatedMovie = new Movie();
                    // Set Movie properties
                    updatedMovie.Id = model.Id;
                    updatedMovie.Title = model.Title;
                    updatedMovie.Plot = model.Plot;
                    updatedMovie.Rating = model.Rating;
                    updatedMovie.ReleaseDate = model.ReleaseDate;
                    updatedMovie.Country = model.Country;
                    updatedMovie.DirectorId = model.DirectorId;
                    updatedMovie.StudioId = model.StudioId;

                    database.Entry(movie).CurrentValues.SetValues(updatedMovie);

                    // remove genres that are not in the list but in DB
                    foreach (var genreInDb in movie.Genres.ToList())
                    {
                        if (!model.SelectedGenres.Contains(genreInDb.Id))
                        {
                            movie.Genres.Remove(genreInDb);
                        }
                    }

                    // Add genres that are not in the DB list but in id list
                    foreach (var genreId in model.SelectedGenres)
                    {
                        if (!movie.Genres.Any(c => c.Id == genreId))
                        {
                            var genre = new Genre { Id = genreId };
                            database.Genres.Attach(genre); 
                            movie.Genres.Add(genre);
                        }
                    }

                    // remove actors that are not in the list but in DB
                    foreach (var actorInDb in movie.Actors.ToList())
                    {
                        if (!model.SelectedActors.Contains(actorInDb.Id))
                        {
                            movie.Actors.Remove(actorInDb);
                        }
                    }

                    // Add actors that are not in the DB list but in id list
                    foreach (var actorId in model.SelectedActors)
                    {
                        if (!movie.Actors.Any(c => c.Id == actorId))
                        {
                            var actor = new Actor { Id = actorId };
                            database.Actors.Attach(actor); 
                            movie.Actors.Add(actor);
                        }
                    }

                    
                    database.Entry(movie).State = EntityState.Modified;
                    database.SaveChanges();

               
                    TempData["Success"] = "Успешно редактирахте филма.";
                    return RedirectToAction("List");
                }
            }

      
            TempData["Danger"] = "Некоректни данни, моля опитайте отново.";
            return View(model);
        }


        // GET: Movie/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
               
                var movie = database.Movies
                    .Where(a => a.Id == id)
                    .First();

             

                
                if (movie == null)
                {
                    return HttpNotFound();
                }

                
                return View(movie);
            }
        }

        // POST: Movie/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MoviesDbContext())
            {
               
                var movie = database.Movies
                    .First(a => a.Id == id);

            
                if (movie == null)
                {
                    return HttpNotFound();
                }

                string fullPathPrimary = Request.MapPath("~/Content/UploadedImages/" + movie.Image);
                if (System.IO.File.Exists(fullPathPrimary))
                {
                    System.IO.File.Delete(fullPathPrimary);
                }
                
                
                database.Movies.Remove(movie);
                database.SaveChanges();
              
                TempData["Success"] = "Успешно изтрихте филма.";
                return RedirectToAction("List");
            }
        }


        private bool IsUserAuthorizedToEdit(Movie article)
        {
            bool isAdmin = this.User.IsInRole("Admin");
      

            return isAdmin;
        }
    }
}