using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Models
{
    public class Director
    {
        public Director()
        {
            this.Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public DateTime? Born { get; set; }
        public string Hometown { get; set; }    
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual Studio Studio { get; set; }
    }
}
