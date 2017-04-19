using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IMDb.Data.DTo;
using IMDb.Data.Store;

namespace IMDb.Import
{
    public static class JsonImport
    {
        public static void ImportMovies()
        {
            var json = File.ReadAllText("../../../datasets/movies.json");
            var movies = JsonConvert.DeserializeObject<IEnumerable<MoviesDTo>>(json);
            MoviesStore.AddMovies(movies);
        }
        public static void ImportGenres()
        {
            var json = File.ReadAllText("../../../datasets/genres.json");
            var genres = JsonConvert.DeserializeObject<IEnumerable<GenresDTo>>(json);
            GenresStore.AddGenres(genres);
        }
       // public static void ImportDirectors()
       // {
       //     var json = File.ReadAllText("../../../datasets/directors.json");
       //     var directors = JsonConvert.DeserializeObject<IEnumerable<DirectorsDTo>>(json);
       //     DirectorsStore.
       // }
    }
}