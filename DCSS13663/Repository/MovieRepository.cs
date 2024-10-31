using DCSS13663.DBContext;
using DCSS13663.Model;
using Microsoft.EntityFrameworkCore;

namespace DCSS13663.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _dbContext;
        public MovieRepository(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteMovie(int movieId)
        {
            var movie = _dbContext.Movies.Find(movieId);
            _dbContext.Movies.Remove(movie);
            Save();
        }
        public Movie GetMovieById(int movieId)
        {
            var m = _dbContext.Movies.Find(movieId);
            _dbContext.Entry(m).Reference(s => s.MovieGenre).Load();
            return m;
        }
        public IEnumerable<Movie> GetMovies()
        {
            return _dbContext.Movies.Include(s => s.MovieGenre).ToList();
        }
        public void InsertMovie(Movie movie)
        {
          
            _dbContext.Movies.Add(movie);
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void UpdateMovie(Movie movie)
        {
            _dbContext.Entry(movie).State =
            EntityState.Modified;
            Save();
        }

    }
}
