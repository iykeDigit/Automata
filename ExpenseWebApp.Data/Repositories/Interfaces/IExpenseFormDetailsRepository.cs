using ExpenseWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWebApp.Data.Repositories.Interfaces
{
    public interface IExpenseFormDetailsRepository : IGenericRepository<ExpenseFormDetails>
    {
        IQueryable<ExpenseFormDetails> GetAllExpenseFormDetails();
        Task<ExpenseFormDetails> GetExpenseFormDetailsById(string formId);
        Task<List<ExpenseFormDetails>> GetAllExpenseFormDetailsByFormId(string formId);
    }
}
