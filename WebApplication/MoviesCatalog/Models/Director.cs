using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Director
    {
        public Director()
        {
            this.Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "Дата на раждане")]
        public DateTime? Born { get; set; }
        [Display(Name = "Страна")]
        public string HomeTown { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
      
    }
}