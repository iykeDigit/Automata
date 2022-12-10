using ExpenseWebApp.Models;
using ExpenseWebApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IUpdateFormStatus
    {
        Task<Response<bool>> SetApprovedStatus(string formId);
        Task<Response<bool>> SetCancelledStatus(string formId);
        Task<Response<bool>> SetDisbursedStatus(string formId);
        Task<Response<bool>> SetFurtherInfoRequiredStatus(string formId, string approverNote);
        Task<Response<bool>> SetNewRequestStatus(string formId);
        Task<Response<bool>> SetPendingApprovalStatus(string formId);
        Task<Response<bool>> SetRejectedStatus(string formId, string approverNote);
        Task<Response<bool>> SetToBeSubmitted(string formId);
        //Task<bool> UpdateDatabase(ExpenseForm expenseForm);
    }
}
