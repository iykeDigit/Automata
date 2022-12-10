using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Dtos.ExpenseAccountDtos;
using ExpenseWebApp.Utilities;
using Microsoft.Extensions.Logging;

namespace ExpenseWebApp.Core.Implementation
{
    public class ExpenseAccountService : IExpenseAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseAccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Get all company accounts
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<Response<ICollection<CompanyAccountsDto>>> GetCompanyAccounts(int companyId)
        {
            var accounts = await _unitOfWork.ExpenseAccount.GetCompanyAccounts(companyId);
            var response = new Response<ICollection<CompanyAccountsDto>>();

            if (accounts.Count < 1)
            {
                response.Succeeded = true;
                response.Data = null;
                response.Message = "No accounts found";
                response.StatusCode = (int)HttpStatusCode.NoContent;
                return response;
            }
            var result = _mapper.Map<ICollection<CompanyAccountsDto>>(accounts);

            response.Data = result;
            response.Succeeded = true;
            response.Message = $"Expense accounts for this company";
            response.StatusCode = (int)HttpStatusCode.OK;
            return response;
        }
    }
}
