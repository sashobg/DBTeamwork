using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Movie
    {

        public Movie()
        {
            this.Genres = new HashSet<Genre>();
            this.Actors = new HashSet<Actor>();

        }
        public Movie(string title, string plot, int genreId)
        {
        
            this.Title = title;
            this.Plot = plot;
            this.Genres = new HashSet<Genre>(); ;
          
        }

        [Key]
        
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Plot { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public double Rating { get; set; }
      
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }

        public int StudioId { get; set; }
        public virtual Studio Studio { get; set; }

        public string Image { get; set; }

    }
}