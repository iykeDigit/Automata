using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.Repositories.Interfaces;
using ExpenseWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Data.Repositories.Implementation
{
    public class CompanyFormDataRepository : GenericRepository<CompanyFormDataRepository>, ICompanyFormDataRepository
    {
        private readonly ExpenseDbContext _dbContext;

        public CompanyFormDataRepository(ExpenseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Gets the data for the form count for the Company
        /// </summary>
        /// <param name="cacNumber"></param>
        /// <returns></returns>
        public CompanyFormData GetFormData(string cacNumber) 
        {
            return _dbContext.CompanyFormData.Where(x => x.CACNumber == cacNumber)
                .SingleOrDefault();
        }

             
        public CompanyFormData AddNewCompanyData(CompanyFormData form)
        {
            try
            {
                _dbContext.CompanyFormData.AddAsync(form);
                _dbContext.SaveChanges();
                return form;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void UpdateCompanyFormData(CompanyFormData form)
        {
            try
            {
                _dbContext.CompanyFormData.Update(form);
               _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCompanyFormData(CompanyFormData form) 
        {
            try
            {
                _dbContext.CompanyFormData.Remove(form);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
