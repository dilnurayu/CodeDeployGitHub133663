using DCSS13663.DBContext;
using DCSS13663.Model;
using Microsoft.EntityFrameworkCore;

namespace DCSS13663.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieContext _dbContext;
        public GenreRepository(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteGenre(int genreId)
        {
            var genre = _dbContext.Genres.Find(genreId);
            _dbContext.Genres.Remove(genre);
            Save();
        }
        public Genre GetGenreById(int genreId)
        {
            var g = _dbContext.Genres.Find(genreId);
            return g;
        }
        public IEnumerable<Genre> GetGenres()
        {
            return _dbContext.Genres.ToList();
        }
        public void InsertGenre(Genre genre)
        {
            _dbContext.Genres.Add(genre);
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void UpdateGenre(Genre genre)
        {
            _dbContext.Entry(genre).State = EntityState.Modified;
            Save();
        }
    }
}
