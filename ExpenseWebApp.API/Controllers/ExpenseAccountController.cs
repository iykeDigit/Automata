using System.Threading.Tasks;
using ExpenseWebApp.Core.Implementation;
using ExpenseWebApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseAccountController : ControllerBase
    {
        private readonly IExpenseAccountService _accountService;

        public ExpenseAccountController(IExpenseAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetAllCompanyAccounts(int companyId)
        {
            var response = await _accountService.GetCompanyAccounts(companyId);
            return StatusCode(response.StatusCode, response);

        }
    }
}
