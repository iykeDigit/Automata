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
    public class ExpenseAccountRepository : GenericRepository<ExpenseAccount>, IExpenseAccountRepository
    {
        private readonly ExpenseDbContext _dbContext;

        public ExpenseAccountRepository(ExpenseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ICollection<ExpenseAccount>> GetCompanyAccounts(int companyId)
        {
            return await _dbContext.ExpenseAccounts.Where(x => x.CompanyId == companyId).ToListAsync();
        }
    }
}
