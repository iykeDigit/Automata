using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.Repositories.Interfaces;
using ExpenseWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWebApp.Data.Repositories.Implementation
{
    public class ExpenseFormRepository : GenericRepository<ExpenseForm>, IExpenseFormRepository
    {
        private readonly ExpenseDbContext _dbContext;
        public ExpenseFormRepository(ExpenseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }



        /// <summary>
        /// Gets all expense forms
        /// </summary>
        /// <returns>all expense forms and their status</returns>
        public IQueryable<ExpenseForm> GetAllExpenseForms()
        {
            return _dbContext.ExpenseForms
                                   .Include(x => x.AdvanceForm)
                                    .Include(x => x.ExpenseStatus)
                                   .Include(x => x.AdvanceRetirement)
                                   .Include(x => x.ExpenseFormDetails).ThenInclude(x => x.ExpenseCategory);

        }

        /// <summary>
        /// Gets an expense form by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single expense form</returns>
        public async Task<ExpenseForm> GetExpenseFormById(string formId)
        {

            return await _dbContext.ExpenseForms
                                    .Include(x => x.AdvanceForm)
                                    .Include(x => x.ExpenseStatus)
                                    .Include(x => x.AdvanceRetirement)
                                    .Include(x => x.ExpenseFormDetails).ThenInclude(x => x.ExpenseCategory)
                                    .FirstOrDefaultAsync(x => x.ExpenseFormId == formId);
        }

        public IQueryable<ExpenseForm> GetExpenseFormsByCompanyId(int companyId)
        {
            return _dbContext.ExpenseForms
                             .Include(x => x.ExpenseStatus)
                             .Include(x => x.AdvanceForm)
                             .Include(x => x.ExpenseFormDetails)
                             .Where(x => x.CompanyId == companyId);
        }

        public async Task<ExpenseForm> GetExpenseForm(string formId)
        {
            return await _dbContext.ExpenseForms
                .Include(x => x.ExpenseFormDetails)
            .FirstOrDefaultAsync(x => x.ExpenseFormId == formId);
        }

        public void UpdateFormStatus(ExpenseForm expenseForm)
        {
            _dbContext.Update(expenseForm.ExpenseStatus);
            _dbContext.Entry(expenseForm).Reference(x => x.ExpenseStatus).IsModified = true;

            _dbContext.SaveChanges();
        }
    }
}