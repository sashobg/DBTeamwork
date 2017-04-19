using IMDb.Data.DTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Data.Store
{
    public static class GenresStore
    {
        public static void AddGenres(IEnumerable<GenresDTo> genres)
        {
            using (var context = new IMDbEntities())
            {
                foreach (var gen in genres)
                {
                    Console.WriteLine($"Succesfully added Genre {gen.name}");
                }
                context.SaveChanges();
            }
        }
    }
}
