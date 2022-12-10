using ExpenseWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Data.Repositories.Interfaces
{
    public interface ICompanyFormDataRepository
    {
        CompanyFormData GetFormData(string cacNumber);
        void UpdateCompanyFormData(CompanyFormData form);
        CompanyFormData AddNewCompanyData(CompanyFormData form);
        void DeleteCompanyFormData(CompanyFormData form);
    }
}
