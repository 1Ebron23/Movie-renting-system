using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Interfaces
{
    public interface IMovieService
    {
        bool IsMovieAvailable(int movieId);

        bool AddMovie(Movie movie);

        bool RemoveMovie(int movieId);

        IEnumerable<Movie> FindMovie(string title, string director, string genre, int? year, string rating, int? duration, int? availableCopies);

        Movie? FindMovies(int movieId);

        List<Movie> GetMovies();
    }
}
