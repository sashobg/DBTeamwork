﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Actor
    {
        public Actor()
        {
            this.Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? Born { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
