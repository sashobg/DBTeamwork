namespace IMDb.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class IMDbEntities : DbContext
    {
        
        public IMDbEntities()
            : base("name=IMDbEntities")
        {
        }

       
         public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Studio> Studios { get; set; }

    }

   
}