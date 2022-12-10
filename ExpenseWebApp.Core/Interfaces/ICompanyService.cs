using ExpenseWebApp.Dtos.CompanyDtos;
using ExpenseWebApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<ClientResponse<GetCompanyResponseDto>> GetCompany(string cacNumber, string token = null);
        Task<ClientResponse<IEnumerable<GetCompanyUsersDto>>> GetCompanyUsers(string cacNumber, string token = null);
    }
}
