using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Interfaces
{
    public interface ILoanService
    {
        bool Loan(Models.LoanInfo loan);
        bool ReturnLoan(int loanId);
        bool UpdateLoan(Models.LoanInfo loan);
    }
}
