using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Import
{
    class Startup
    {
        static void Main(string[] args)
        {
            JsonImport.ImportMovies();
            //JsonImport.ImportGenres();
        }
    }
}
