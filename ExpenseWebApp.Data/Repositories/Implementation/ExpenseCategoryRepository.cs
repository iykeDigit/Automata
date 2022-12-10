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
    public class ExpenseCategoryRepository : GenericRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        private readonly ExpenseDbContext _dbContext;

        public ExpenseCategoryRepository(ExpenseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public ExpenseCategory GetExpenseCategoryByName(string name)
        {
            return  _dbContext.ExpenseCategories.FirstOrDefault(x => x.ExpenseCategoryName.ToLower() == name.ToLower());
        }

        public IQueryable<ExpenseCategory> GetAllExpenseCategory() 
        {
            return _dbContext.ExpenseCategories;
        }
    }
}
