using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.Repositories.Interfaces;
using ExpenseWebApp.Models;

namespace ExpenseWebApp.Data.Repositories.Implementation
{
    public class PaidFormRepository : GenericRepository<PaidFrom>, IPaidFormRepository
    {
        private readonly ExpenseDbContext _dbContext;

        public PaidFormRepository(ExpenseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
