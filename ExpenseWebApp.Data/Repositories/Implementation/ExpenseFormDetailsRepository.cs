using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.Repositories.Interfaces;
using ExpenseWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWebApp.Data.Repositories.Implementation
{
    public class ExpenseFormDetailsRepository : GenericRepository<ExpenseFormDetails>, IExpenseFormDetailsRepository
    {
        private readonly ExpenseDbContext _dbContext;
        private readonly DbSet<ExpenseFormDetails> _dbSet;

        public ExpenseFormDetailsRepository(ExpenseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ExpenseFormDetails>();
        }

        /// <summary>
        /// Gets all expense form details
        /// </summary>
        /// <returns>all expense form details</returns>
        public IQueryable<ExpenseFormDetails> GetAllExpenseFormDetails()
        {
            return _dbContext.ExpenseFormDetails
                                   .Include(x => x.ExpenseCategory)
                                   .Include(x => x.ExpenseForm);
        }

        /// <summary>
        /// Gets all expense form details for a particular form
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<ExpenseFormDetails>> GetAllExpenseFormDetailsByFormId(string formId)
        {
            return await _dbContext.ExpenseFormDetails
                                   .Include(x => x.ExpenseCategory)
                                   .Include(x => x.ExpenseForm)
                                   .Where(x => x.ExpenseFormId == formId).ToListAsync();
        }

        /// <summary>
        /// Gets an expense form details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a single expense form details</returns>
        public async Task<ExpenseFormDetails> GetExpenseFormDetailsById(string formId)
        {
            return await _dbContext.ExpenseFormDetails
                            .Include(x => x.ExpenseCategory)
                            .Include(x => x.ExpenseForm)
                            .ThenInclude(x => x.ExpenseStatus)
                            .FirstOrDefaultAsync(x => x.ExpenseFormDetailsId == formId);
        }
    }
}
