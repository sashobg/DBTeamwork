using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Movies.Models
{

    public class MoviesDbContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }


        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Studio> Studios { get; set; }

        

        public static MoviesDbContext Create()
        {
            return new MoviesDbContext();
        }

      
    }
}