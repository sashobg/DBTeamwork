﻿using System.ComponentModel.DataAnnotations;

namespace IMDb.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}