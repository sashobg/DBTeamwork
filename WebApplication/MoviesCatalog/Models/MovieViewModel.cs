using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Models
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Plot { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public double Rating { get; set; }

        public virtual List<int> SelectedGenres { get; set; }

        public IEnumerable<SelectListItem> AllGenres { get; internal set; }

        public virtual List<int> SelectedActors { get; set; }
        public IEnumerable<SelectListItem> AllActors { get; internal set; }

        public int DirectorId { get; set; }

        public IEnumerable<SelectListItem> AllDirectors { get; internal set; }



        public int StudioId { get; set; }
        public IEnumerable<SelectListItem> AllStudios { get; internal set; }

    }
}