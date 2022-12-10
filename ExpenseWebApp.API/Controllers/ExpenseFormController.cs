using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Dtos;
using ExpenseWebApp.Dtos.ExpenseFormDetailsDtos;
using ExpenseWebApp.Dtos.ExpenseFormDto;
using ExpenseWebApp.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseFormController : ControllerBase
    {
        private readonly IExpenseFormService _expenseFormService;
        private readonly ILogger<ExpenseFormController> _logger;

        public ExpenseFormController(IExpenseFormService expenseFormService,
            ILogger<ExpenseFormController> logger)
        {
            _expenseFormService = expenseFormService;
            _logger = logger;
        }

        [HttpPut("{formDetailId}/edit-form-detail")]
        public async Task<ActionResult<Response<bool>>> EditExpenseForm(string formDetailId, [FromBody] ExpenseFormDetailDto editExpenseFormDto)
        {
            var result = await _expenseFormService.EditExpenseFormDetail(formDetailId, editExpenseFormDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{formId}/save-form")]
        public async Task<ActionResult<Response<IEnumerable<ExpenseFormDetailResponseDto>>>> SaveExpenseForm(string formId, [FromBody] List<ExpenseFormDetailDto> expenses)
        {
            var result = await _expenseFormService.SaveExpenseForm(formId, expenses);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{formId}/submit-form")]
        public async Task<ActionResult<Response<bool>>> SubmitExpenseForm([FromRoute]string formId,  [FromQuery]string cacNumber, [FromBody]List<ExpenseFormDetailDto> expenses)
        {
            var result = await _expenseFormService.SubmitExpenseForm(formId, cacNumber, expenses);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{formId}/cacNumber={cacNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExpenseFormById(string formId, string cacNumber)
       {
            var response = await _expenseFormService.GetExpenseFormById(formId, cacNumber);
            return StatusCode(response.StatusCode, response);

        }

        [HttpGet("Sumbitted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadFormsSubmittedForApproval([FromQuery] PagingDto paging)
        {
            var response = await _expenseFormService.GetAllSubmittedExpenseFormsAsync(paging);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("PaidByEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadApprovedFormsPaidByEmployeesAsync([FromQuery] PagingDto paging)
        {
            var response = await _expenseFormService.GetApprovedExpenseFormsPaidByEmployeeAsync(paging);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Employee/cacNumber={cacNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadEmployeeExpenseForms([FromQuery] PagingDto paging, int userId, string cacNumber)
        {
            var response = await _expenseFormService.GetEmployeeExpenseFormsAsync(paging, userId, cacNumber);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("DiscardExpenseDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DiscardExpenseFormsAsync(string formId)
        {
            var response = await _expenseFormService.DiscardExpenseFormsAsync(formId);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPatch("{cacNumber}/reimburse-expense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReimburseExpense(string formId, string cacNumber, string token)
        {
            _logger.LogInformation($"Geting information for the expense with formId {formId} for disbursement");
            var response = await _expenseFormService.ReimburseExpense(formId, cacNumber, token);
            _logger.LogInformation($"Gotten information for the expense with expense formId {formId}");
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("further-information-required")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FurtherInfoRequired(string approverNote, string formId, string cacNumber, string token)
        {
            _logger.LogInformation($"Geting information for the expense with formId {formId}");
            var response = await _expenseFormService.FurtherInfoRequired(approverNote, formId, cacNumber, token);
            _logger.LogInformation($"Gotten information for the expense with expense formId {formId}");
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("cancel-expense-form")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelExpenseForm(string formId)
        {
            _logger.LogInformation($"Retrieving the expense form with ID {formId}");
            var response = await _expenseFormService.CancelExpenseForm(formId);
            _logger.LogInformation($"Gotten information for the expense with expense formId {formId}");
            return StatusCode(response.StatusCode, response);
        }


        [HttpPatch("{formId}/approve-form")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<bool>>> ApproveExpenseForm(string formId, string cacNumber, string token)
        {
            var result = await _expenseFormService.ApproveExpenseForm(formId, cacNumber, token);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{cacNumber}/approved-expense-forms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<bool>>> GetAllApprovedExpenseForms([FromQuery] PagingDto pagingDto, string cacNumber)
        {
            var result = await _expenseFormService.GetAllApprovedForms(pagingDto, cacNumber);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{formId}/reject-form")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<bool>>> RejectExpenseForm(string approverNote, string formId, string cacNumber, string token)
        {
            var result = await _expenseFormService.RejectExpenseForm(approverNote, formId, cacNumber, token);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("create-new-expenseForm/cacNumber={cacNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Response<ExpenseFormCreateResponseDto>>> CreateExpenseForm([FromBody]ExpenseFormCreateRequestDto model, string cacNumber)
        
        {
            var result = await _expenseFormService.CreateExpenseForm(model, cacNumber);
            return StatusCode(result.StatusCode, result);
        }         
    }
}