using ExpenseWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Data.Repositories.Interfaces
{
    public interface IExpenseStatusRepository
    {
        Task<ExpenseStatus> GetExpenseStatusByDescription(string description);
    }
}
