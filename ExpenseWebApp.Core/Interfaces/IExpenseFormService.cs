using ExpenseWebApp.Dtos;
using ExpenseWebApp.Dtos.ExpenseFormDetailsDtos;
using ExpenseWebApp.Dtos.ExpenseFormDto;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IExpenseFormService
    {
        Task<Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>> GetAllApprovedForms(PagingDto paging, string cacNumber);
        Task<Response<bool>> EditExpenseFormDetail(string id, ExpenseFormDetailDto editExpenseFormDto);
        Task<Response<IEnumerable<ExpenseFormDetailResponseDto>>> SaveExpenseForm(string formId, List<ExpenseFormDetailDto> expenses);
        Task<Response<bool>> SubmitExpenseForm(string formId, string cacNumber, List<ExpenseFormDetailDto> expenses);
        Task<Response<ExpenseFormResponseDto>> GetExpenseFormById(string formId, string cacNumber);
        Task<Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>> GetEmployeeExpenseFormsAsync(PagingDto paging, int userId, string cacNumber);
        Task<Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>> GetApprovedExpenseFormsPaidByEmployeeAsync(PagingDto paging);
        Task<Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>> GetAllSubmittedExpenseFormsAsync(PagingDto paging);
        Task<Response<string>> DiscardExpenseFormsAsync(string formId);
        Task<Response<ExpenseFormResponseDto>> ApproveExpenseForm(string formId, string cacNumber, string token);
        Task<Response<ExpenseFormResponseDto>> RejectExpenseForm(string ApproverNote, string formId, string cacNumber, string token);
        Task<Response<ExpenseFormResponseDto>> FurtherInfoRequired(string approverNote, string formId, string cacNumber, string token);
        Task<Response<ExpenseFormResponseDto>> ReimburseExpense(string formId, string cacNumber, string token);
        Task<Response<ExpenseFormResponseDto>> CancelExpenseForm(string formId);
        Task<Response<ExpenseFormCreateResponseDto>> CreateExpenseForm(ExpenseFormCreateRequestDto expenseFormCreateDto, string cacNumber);
      



    }
}