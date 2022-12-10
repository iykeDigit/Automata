using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Models;

namespace ExpenseWebApp.Data.Repositories.Interfaces
{
    public interface IAdvanceRetirementRepository : IGenericRepository<AdvanceRetirement>
    {
        Task<AdvanceRetirement> GetAdvanceRetirementFormById(string formId);
        IQueryable<AdvanceRetirement> GetAllAdvanceRetirementForms();
    }
}
