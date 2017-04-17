using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDb.Models
{
    public class Actor
    {
        public Actor()
        {
            this.Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public DateTime? Born { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}