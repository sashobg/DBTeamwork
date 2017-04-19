using IMDb.Data;
using IMDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Client
{
    class Startup
    {
        static void Main(string[] args)
        {
            //Utility.InitDB();
            var context = new IMDbEntities();
            ListAllMovies(context);
            

        }

        static void ListAllMovies(IMDbEntities context)
        {
            int pageSize = 14;

            var movies = context.Movies.ToList();
            int page = 0;
            int maxPages = (int)Math.Ceiling(movies.Count / (double)pageSize);
            int pointer = 1;

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();
                Console.WriteLine($" ID | Movie Title (Page {page + 1} of {maxPages})");
                Console.WriteLine("----+----------------------------");

                int current = 1;
                foreach (var mov in movies.Skip(pageSize * page).Take(pageSize))
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    if (current == pointer)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($"{mov.Title} | {mov.Director}");
                    current++;
                }

                var key = Console.ReadKey();

                switch (key.Key.ToString())
                {
                    case "Enter":
                        var currentMovie = movies.Skip(pageSize * page + pointer - 1).First();
                        ShowDetails(currentMovie);
                        Console.WriteLine("Enter pressed");
                        break;
                    case "UpArrow":
                        if (pointer > 1)
                        {
                            pointer--;
                        }
                        else if (page > 0)
                        {
                            page--;
                            pointer = pageSize;
                        }
                        break;
                    case "DownArrow":
                        if (pointer < pageSize)
                        {
                            pointer++;
                        }
                        else if (page + 1 <= maxPages)
                        {
                            page++;
                            pointer = 1;
                        }
                        break;
                    case "Escape":
                        return;
                }
            }
        }

        static void ShowDetails(Movie movie)
        {
            
            Console.Clear();
            Console.WriteLine($"{movie.Studio}");
            Utility.PrintHLine();
            Console.WriteLine($"{movie.Director}");
            Utility.PrintHLine();
            Console.WriteLine($"{movie.Actors}");
            Utility.PrintHLine();
            Console.WriteLine("PLOT");
            Utility.PrintHLine();
            Console.WriteLine($"{movie.Plot}");
            Console.ReadKey();
            int pageSize = 16 - Console.CursorTop;

            var actors = movie.Actors.ToList();
            int page = 0;
            int maxPages = (int)Math.Ceiling(actors.Count / (double)pageSize);
            int pointer = 1;

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();
                Console.WriteLine($"{movie.Studio}");
                Utility.PrintHLine();
                Console.WriteLine($"{movie.Director}");
                Utility.PrintHLine();
                Console.WriteLine($"{movie.Actors}");
                Utility.PrintHLine();
                Console.WriteLine("PLOT");
                Utility.PrintHLine();
                Console.WriteLine($"{movie.Plot}");
                Console.WriteLine($"Page {page + 1} of {maxPages})");
                Console.WriteLine("---------------------------------");

                int current = 1;
                foreach (var act in actors.Skip(pageSize * page).Take(pageSize))
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    if (current == pointer)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(act);
                    current++;
                }

                var key = Console.ReadKey();

                switch (key.Key.ToString())
                {
                    /*
                    case "Enter":
                        var currentProject = employees.Skip(pageSize * page + pointer - 1).First();
                        ShowDetails(currentProject);
                        Console.WriteLine("Enter pressed");
                        break; */
                    case "UpArrow":
                        if (pointer > 1)
                        {
                            pointer--;
                        }
                        else if (page > 0)
                        {
                            page--;
                            pointer = pageSize;
                        }
                        break;
                    case "DownArrow":
                        if (pointer < pageSize)
                        {
                            pointer++;
                        }
                        else if (page + 1 <= maxPages)
                        {
                            page++;
                            pointer = 1;
                        }
                        break;
                    case "Escape":
                        return;
                }
            }
        }
    }
}