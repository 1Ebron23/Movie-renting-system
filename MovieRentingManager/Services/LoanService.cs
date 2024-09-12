using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Services
{
    public class BookLoanService : ILoanService
    {
        private readonly IMovieService movieService;
        private List<LoanInfo> loans = new List<LoanInfo>();

        public BookLoanService(IMovieService service)
        {
            movieService = service;
        }

        public bool Loan(LoanInfo loan)
        {
            Movie movie = movieService.FindMovies(loan.BookId);

            if (movieService.IsMovieAvailable(movie.Id) == false)
                return false;

            movie.AvailableCopies--;

            loans.Add(loan);

            return true;
        }

        public bool ReturnLoan(int loanId)
        {
            LoanInfo? loanInfo = loans.FirstOrDefault(l => l.Id == loanId);

            if (loanInfo == null)
                return false;

            Movie movie = movieService.FindMovies(loanInfo.BookId);
            movie.AvailableCopies++;

            return true;
        }

        public bool UpdateLoan(LoanInfo loan)
        {
            throw new NotImplementedException();
        }
    }
}
