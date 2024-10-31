using DCSS13663.Model;

namespace DCSS13663.Repository
{
    public interface IGenreRepository
    {
        void InsertGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int genreId);
        Genre GetGenreById(int Id);
        IEnumerable<Genre> GetGenres();
    }
}
