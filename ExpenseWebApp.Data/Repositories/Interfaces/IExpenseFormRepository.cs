using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Models;

namespace ExpenseWebApp.Data.Repositories.Interfaces
{
    public interface IExpenseFormRepository : IGenericRepository<ExpenseForm>
    {
        IQueryable<ExpenseForm> GetAllExpenseForms();
        Task<ExpenseForm> GetExpenseFormById(string formId);
        IQueryable<ExpenseForm> GetExpenseFormsByCompanyId(int companyId);
        Task<ExpenseForm> GetExpenseForm(string formId);
        void UpdateFormStatus(ExpenseForm expenseForm);
    }
}
