using ExpenseWebApp.Dtos;
using ExpenseWebApp.Dtos.ExpenseAdvanceDtos;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IExpenseAdvance
    {
        /// <summary>
        /// Submit the Advance for approval
        /// </summary>
        /// <param name="expenseAdvanceDto"></param>
        /// <returns></returns>
        Task<Response<string>> SubmitAdvanceRequest(SubmitExpenseAdvanceDto expenseAdvanceDto);
        /// <summary>
        /// Creates new cash advance form to be submitted for approval
        /// </summary>
        /// <param name="advanceDto"></param>
        /// <returns>Form successfully saved</returns>
        Task<Response<string>> CreateCashAdvance(CreateExpenseAdvanceDto advanceDto);
        /// <summary>
        /// Gets all Expense Advance forms pending Approval
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<Response<PaginatorHelper<IEnumerable<ExpenseAdvanceReturnDto>>>> GetPendingRequestsAsync(PagingDto paging, int companyId);
        /// <summary>
        /// Updates the AdvanceRequest form for an approver
        /// </summary>
        /// <param name="updateExpense"></param>
        /// <param name="formId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<Response<bool>> UpdateAdvanceRequestForApproverAsync(string formId, UpdateExpenseAdvanceDto updateExpense);
        Task<Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>> GetApprovedCashAdvanceExpenseForms(PagingDto paging);
        Task<Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>> GetRequestorExpenseAdvanceForms(PagingDto paging, int userId, string cacNumber);
        Task<Response<bool>> EditCashAdvance(string formDetailId, EditExpenseAdvanceDto editAdvanceDto);
        Task<Response<string>> DiscardFormsAsync(string formId);

    }
}
