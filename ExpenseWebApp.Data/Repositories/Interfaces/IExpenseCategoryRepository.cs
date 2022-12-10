using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Models;

namespace ExpenseWebApp.Data.Repositories.Interfaces
{
    public interface IExpenseCategoryRepository : IGenericRepository<ExpenseCategory>
    {
        ExpenseCategory GetExpenseCategoryByName(string name);

        IQueryable<ExpenseCategory> GetAllExpenseCategory();
    }
}
