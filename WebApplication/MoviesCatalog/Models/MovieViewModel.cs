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
        [Display(Name = "Заглавие")]
        public string Title { get; set; }
        [Display(Name = "Резюме")]
        public string Plot { get; set; }
        [Display(Name = "Премиера")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Държава")]
        public string Country { get; set; }
        [Display(Name = "Рейтинг")]
        public double Rating { get; set; }
        [Required]
        public virtual List<int> SelectedGenres { get; set; }
        [Display(Name = "Жанрове")]
        public IEnumerable<SelectListItem> AllGenres { get; internal set; }
        [Required]
        public virtual List<int> SelectedActors { get; set; }
        [Display(Name = "Актьори")]
        public IEnumerable<SelectListItem> AllActors { get; internal set; }
        [Required]
        [Display(Name = "Режисьор")]
        public int DirectorId { get; set; }
       
        public IEnumerable<SelectListItem> AllDirectors { get; internal set; }

        [Required]
        [Display(Name = "Студио")]
        public int StudioId { get; set; }
        
        public IEnumerable<SelectListItem> AllStudios { get; internal set; }

    }
}