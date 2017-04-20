using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Genre
    {
        public Genre()
        {
                this.Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Жанр")]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}