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
    public class AdvanceRetirementRepository : GenericRepository<AdvanceRetirement>, IAdvanceRetirementRepository
    {
        private readonly ExpenseDbContext _dbContext;

        public AdvanceRetirementRepository(ExpenseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a single advance retirement form
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an advance retirement form</returns>
        public async Task<AdvanceRetirement> GetAdvanceRetirementFormById(string formId)
        {
            return await _dbContext.AdvanceRetirements
                                    .Include(x => x.ExpenseForm)
                                    .Include(x => x.PaidFrom)
                                    .Include(x => x.ExpenseStatus)
                                    .Include(x => x.AdvanceForm)
                                    .FirstOrDefaultAsync(x => x.AdvanceRetirementId == formId);
        }

        /// <summary>
        /// Gets all advance retirement forms
        /// </summary>
        /// <returns>all advance retirement forms</returns>
        public IQueryable<AdvanceRetirement> GetAllAdvanceRetirementForms()
        {
            return _dbContext.AdvanceRetirements
                                    .Include(x => x.ExpenseForm)
                                    .Include(x => x.ExpenseStatus)
                                    .Include(x => x.PaidFrom)
                                    .Include(x => x.AdvanceForm);
        }

    }
}
