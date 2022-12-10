using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseWebApp.Data.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ExpenseDbContext _dbContext;
        private readonly DbSet<T> _table;
        public GenericRepository(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }

        public async Task AddAsync(T data)
        {
            await _table.AddAsync(data);
        }

        public void Attach(T data)
        {
            _table.Attach(data);
        }

        public void AddRange(ICollection<T> data)
        {
            _table.AddRange(data);
        }

        public void Delete(T data)
        {
            _table.Remove(data);
        }

        public void Update(T data)
        {
            _table.Update(data);
        }
    }
}
