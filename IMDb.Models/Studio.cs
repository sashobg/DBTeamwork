using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Models
{
    public class Studio
    {
        public Studio()
        {
            this.Movies = new HashSet<Movie>();
            this.Directors = new HashSet<Director>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Director>Directors { get; set; }
    }
}
