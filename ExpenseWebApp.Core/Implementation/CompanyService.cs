using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Dtos.CompanyDtos;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.HttpClientService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly IHttpClientService _httpClientService;

        public CompanyService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        /// <summary>
        /// Gets the company info of the provided CAC number
        /// </summary>
        /// <param name="cacNumber"></param>
        /// <returns>Returns an instance of ClientReponse<GetCompanyResponseDto></returns>
        public async Task<ClientResponse<GetCompanyResponseDto>> GetCompany(string cacNumber, string token = null)
        {
            return await _httpClientService.GetRequestAsync
                <ClientResponse<GetCompanyResponseDto>>(
                    requestUrl: $"api/Company/cac?cacNumber={cacNumber}", null, token);
        }

        /// <summary>
        /// Gets the company info of the provided CAC number
        /// </summary>
        /// <param name="cacNumber"></param>
        /// <returns>Returns an instance of ClientReponse<GetCompanyResponseDto></returns>
        public async Task<ClientResponse<IEnumerable<GetCompanyUsersDto>>> GetCompanyUsers(string cacNumber, string token = null)
        {
            return await _httpClientService.GetRequestAsync
                <ClientResponse<IEnumerable<GetCompanyUsersDto>>>(
                    requestUrl: $"api/Authentication/get-users-company?cacNumber={cacNumber}", null, token);
        }
    }
}
