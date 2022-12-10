using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Dtos;
using ExpenseWebApp.Dtos.ExpenseAdvanceDtos;
using ExpenseWebApp.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenseWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseAdvanceController : ControllerBase
    {
        private readonly IExpenseAdvance _expenseAdvance;

        public ExpenseAdvanceController(IExpenseAdvance expenseAdvance)
        {
            _expenseAdvance = expenseAdvance;
        }

        [HttpPost("SubmitAdvanceRequest")]
        public async Task<IActionResult> SubmitAdvanceRequest(SubmitExpenseAdvanceDto submitExpenseAdvanceDto)
        {
            var result = await _expenseAdvance.SubmitAdvanceRequest(submitExpenseAdvanceDto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("CreateCashAdvanceForm")]

        public async Task<IActionResult> CreateCashAdvance(CreateExpenseAdvanceDto expenseAdvanceDto)
        {
            var result = await _expenseAdvance.CreateCashAdvance(expenseAdvanceDto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Returns all pending ExpenseAdvance requests in a paginated format.
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns>A list of all pending ExpenseAdvance requests in a paginated format.</returns>
        [HttpGet]
        [Route("pendingrequests/{companyId}")]
        public async Task<IActionResult> GetPendingExpenseAdvanceRequestsAsync([FromQuery] PagingDto searchQuery, int companyId)
        {
            var result = await _expenseAdvance.GetPendingRequestsAsync(searchQuery, companyId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Updates the status of an ExpenseAdvance request.
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="updateExpense"></param>
        /// <returns>A response message that shows details if the request was successful or failed</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch]
        [Route("update/{formId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateExpenseAdvanceRequestAsync(string formId, UpdateExpenseAdvanceDto updateExpense)
        {
            Response<bool> result = await _expenseAdvance.UpdateAdvanceRequestForApproverAsync(formId, updateExpense);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllApprovedExpenseAdvanceForms([FromQuery] PagingDto paging, [FromQuery]string status)
        {
            var response = await _expenseAdvance.GetApprovedCashAdvanceExpenseForms(paging);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Requestor/cacNumber={cacNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadRequestorExpenseAdvanceForms([FromQuery] PagingDto paging, int userId, string cacNumber)
        {
            var response = await _expenseAdvance.GetRequestorExpenseAdvanceForms(paging, userId, cacNumber);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPut("{formDetailId}/edit-form-detail")]
        public async Task<IActionResult> EditCashAdvance(EditExpenseAdvanceDto expenseAdvanceDto, string formDetailId)
        {
            var result = await _expenseAdvance.EditCashAdvance(formDetailId, expenseAdvanceDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("DiscardExpenseDetails")]
        public async Task<IActionResult> DiscardExpenseFormsAsync(string formId)
        {
            var response = await _expenseAdvance.DiscardFormsAsync(formId);
            return StatusCode(response.StatusCode, response);
        }


    }
}