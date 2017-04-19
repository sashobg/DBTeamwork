using Movies.Migrations;
using Movies.Models;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(Movies.Startup))]
namespace Movies
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<MoviesDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
