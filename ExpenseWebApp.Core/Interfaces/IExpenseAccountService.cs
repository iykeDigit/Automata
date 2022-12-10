using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Dtos.ExpenseAccountDtos;
using ExpenseWebApp.Utilities;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IExpenseAccountService
    {
        Task<Response<ICollection<CompanyAccountsDto>>> GetCompanyAccounts(int companyId);
    }
}
