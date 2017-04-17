using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Data
{
    public static class Utility
    {
        public static void InitDB()
        {
            var context = new IMDbEntities();
            context.Database.Initialize(true);
        }
        public static void PrintHLine()
        {
            Console.Write(new string('-', Console.WindowWidth));
        }
    }
}
