using DCSS13663.Model;

namespace DCSS13663.Repository
{
    public interface IMovieRepository
    {
        void InsertMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int movieId);
        Movie GetMovieById(int Id);
        IEnumerable<Movie> GetMovies();
    }
}
