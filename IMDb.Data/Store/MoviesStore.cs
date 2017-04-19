using IMDb.Data.DTo;
using IMDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Data.Store
{
    public static class MoviesStore
    {
        public static void AddMovies(IEnumerable<MoviesDTo> movies)
        {
            using (var context = new IMDbEntities())
            {
                foreach (var MoviesDTo in movies)
                {
                    Console.WriteLine($"Succesfully added movie {MoviesDTo.title}");
                }
                context.SaveChanges();
            }
        }
    }
}
