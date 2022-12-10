using ExpenseWebApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IExpenseFormDetails
    {
       Task<Response<string>> DeleteExpenseDetail(string expenseDetailId);
    }
}
