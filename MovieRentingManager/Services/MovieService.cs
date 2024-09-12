using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieRentingManager.Services
{
    public class MovieService : IMovieService
    {
        private List<Movie> movies = new List<Movie>();

        public MovieService()
        {
            //Add some initial movies
            movies.Add(new Movie
            {
                Id = 1,
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                Director = "Peter Jackson",
                Genre = "Fantasy",
                ReleaseYear = 2001,
                Rating = "4.8", // Rating between 0 and 5 as a string
                Duration = 178, // in minutes
                Description = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
                AvailableCopies = 5
            });

            movies.Add(new Movie
            {
                Id = 3,
                Title = "Inception",
                Director = "Christopher Nolan",
                Genre = "Science Fiction",
                ReleaseYear = 2010,
                Rating = "4.7",
                Duration = 148,
                Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                AvailableCopies = 7
            });

            movies.Add(new Movie
            {
                Id = 4,
                Title = "The Matrix",
                Director = "Lana Wachowski, Lilly Wachowski",
                Genre = "Action",
                ReleaseYear = 1999,
                Rating = "4.6",
                Duration = 136,
                Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                AvailableCopies = 6
            });

            
            
        }

        public bool AddMovie(Movie movie)
        {
            movie.Id = GetNextId();
            movies.Add(movie);

            return true;
        }

        public IEnumerable<Movie> FindMovie(string title, string director, string genre, int? year, string rating, int? duration, int? availableCopies)
        {
            // Build a new queryable object from the list of movies
            IQueryable<Movie> query = movies.AsQueryable();

            
            title = title?.Trim();
            director = director?.Trim();
            genre = genre?.Trim();
            rating = rating?.Trim();

            // Add query filter based on the input parameters
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(m => m.Title.ToLower().Contains(title.ToLower()));
            }

            else if (!string.IsNullOrEmpty(director))
            {
                query = query.Where(m => m.Director.ToLower().Contains(director.ToLower()));
            }

            else if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(m => m.Genre.ToLower().Contains(genre.ToLower()));
            }

            else if (year.HasValue && year > 0)
            {
                query = query.Where(m => m.ReleaseYear == year);
            }

            else if (!string.IsNullOrEmpty(rating))
            {
                query = query.Where(m => m.Rating.ToLower() == rating.ToLower()); 
            }

            else if (duration.HasValue)
            {
                query = query.Where(m => m.Duration == duration);
            }

            else if (availableCopies.HasValue)
            {
                query = query.Where(m => m.AvailableCopies >= availableCopies);
            }

            return query.ToList();
        }


        public Movie? FindMovies(int movieId)
        {
            return movies.FirstOrDefault(b => b.Id == movieId);
        }

        public List<Movie> GetMovies()
        {
            return movies;
        }

        public bool IsMovieAvailable(int movieId)
        {
            Movie? movie = movies.FirstOrDefault(b => b.Id == movieId);

            if (movie == null)
            {
                return false;
            }

            if (movie.AvailableCopies == 0)
            {
                return false;
            }

            return true;
        }

        public bool RemoveMovie(int movieId)
        {
            Movie? movieToRemove = movies.FirstOrDefault(b => b.Id == movieId);

            if(movieToRemove == null)
            {
                return false;
            }

            movies.Remove(movieToRemove);

            return true;
        }

        private int GetNextId()
        {
            return movies.Max(x => x.Id) + 1;
        }
    }
}
