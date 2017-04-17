using IMDb.Data;
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
            Utility.InitDB();

        }
        static void ListAll(IMDbEntities context)
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
                    Console.WriteLine($"123");
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
        static void ShowDetails(Models.Movie Movie)
        {
            //-------------------------------------------------------
            Console.Clear();
            Console.WriteLine($"ID: {Movie.Title}| {Movie.Director}");
            Utility.PrintHLine();
            Console.WriteLine(Movie.Plot);
            Utility.PrintHLine();
            Console.WriteLine($"Page ");
            Console.WriteLine("---------------------------------");
            Console.ReadKey();
            int pageSize = 16 - Console.CursorTop;

            var actors = Movie.Actors.ToList();
            int page = 0;
            int maxPages = (int)Math.Ceiling(actors.Count / (double)pageSize);
            int pointer = 1;

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();
                Console.WriteLine($"ID: {Movie.Title,4}| {Movie.Director}");
                Utility.PrintHLine();
                Console.WriteLine(Movie.Plot);
                Utility.PrintHLine();
                Console.WriteLine($"Page {page + 1} of {maxPages})");
                Console.WriteLine("---------------------------------");

                int current = 1;
       // foreach (var mov in Movie.Skip(pageSize * page).Take(pageSize))
       // {
       //     Console.BackgroundColor = ConsoleColor.Black;
       //     Console.ForegroundColor = ConsoleColor.White;
       //     if (current == pointer)
       //     {
       //         Console.BackgroundColor = ConsoleColor.White;
       //         Console.ForegroundColor = ConsoleColor.Black;
       //     }
       //     Console.WriteLine(mov);
       //     current++;
       // }
       //
       // var key = Console.ReadKey();
       //
       // switch (key.Key.ToString())
       // {
       //     case "UpArrow":
       //         if (pointer > 1)
       //         {
       //             pointer--;
       //         }
       //         else if (page > 0)
       //         {
       //             page--;
       //             pointer = pageSize;
       //         }
       //         break;
       //     case "DownArrow":
       //         if (pointer < pageSize)
       //         {
       //             pointer++;
       //         }
       //         else if (page + 1 <= maxPages)
       //         {
       //             page++;
       //             pointer = 1;
       //         }
       //         break;
       //     case "Escape":
       //         return;
       // }
            }
        }
    }
}

