
using problem2.Models;

namespace problem2.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetMovies();
        Movie GetById(int id);
        void CreateMovie(Movie movie);
        void EditMovie(Movie movie);
        void DeleteMovie(int id);
    }
}