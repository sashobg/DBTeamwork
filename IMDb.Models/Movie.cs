using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Genres = new HashSet<Genre>();
            this.Actors = new HashSet<Actor>();
            
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Country { get; set; }
        public double Rating { get; set; }
        public string Plot { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Actor>Actors { get; set; }
        public virtual Director Director { get; set; }
        public virtual Studio Studio { get; set; }
    }
}
