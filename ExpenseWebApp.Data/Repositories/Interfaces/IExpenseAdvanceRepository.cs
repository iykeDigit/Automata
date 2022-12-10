using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Models;

namespace ExpenseWebApp.Data.Repositories.Interfaces
{
    public interface IExpenseAdvanceRepository : IGenericRepository<ExpenseAdvance>
    {
        IQueryable<ExpenseAdvance> GetAllExpenseAdvanceForms();
        Task<ExpenseAdvance> GetExpenseAdvanceById(string formId);
    }
}
