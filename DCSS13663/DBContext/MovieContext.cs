using DCSS13663.Model;
using Microsoft.EntityFrameworkCore;

namespace DCSS13663.DBContext
{
    public class MovieContext : DbContext
    {
        //Constructors
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
