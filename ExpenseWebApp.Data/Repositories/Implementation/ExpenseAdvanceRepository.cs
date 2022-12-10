using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.Repositories.Interfaces;
using ExpenseWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseWebApp.Data.Repositories.Implementation
{
    public class ExpenseAdvanceRepository : GenericRepository<ExpenseAdvance>, IExpenseAdvanceRepository
    {
        private readonly ExpenseDbContext _dbContext;
        public ExpenseAdvanceRepository(ExpenseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets an expense advance form
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a single form</returns>
        public async Task<ExpenseAdvance> GetExpenseAdvanceById(string formId)
        {
            return await _dbContext.ExpenseAdvance
                                   .Include(x => x.AdvanceRetirement)
                                   .Include(x => x.PaidFrom)
                                   .Include(x => x.ExpenseStatus)
                                   .FirstOrDefaultAsync(x => x.AdvanceFormId == formId);
        }

        /// <summary>
        /// Gets all expense advance forms
        /// </summary>
        /// <returns>all expense advance forms</returns>
        public IQueryable<ExpenseAdvance> GetAllExpenseAdvanceForms()
        {

            return _dbContext.ExpenseAdvance
                                   .Include(x => x.ExpenseStatus)
                                   .Include(x => x.AdvanceRetirement)
                                   .Include(x => x.PaidFrom);
        }


    }
}
