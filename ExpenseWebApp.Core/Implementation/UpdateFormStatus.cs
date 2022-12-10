using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Models;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.ResourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class UpdateFormStatus : IUpdateFormStatus
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExpenseDbContext _dbContext;

        public UpdateFormStatus(IUnitOfWork unitOfWork, ExpenseDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        /// <summary>
        /// This method update's the status of the form to approve
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>bool</returns>
        public async Task<Response<bool>> SetApprovedStatus(string formId)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);
            if (expenseForm != null)
            {
                var status = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.Approved);

                if(status != null) 
                {
                    expenseForm.ExpenseStatus = status;
                    bool result = await UpdateDatabase(expenseForm);
                    if (result)
                    {
                        return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
                    }
                    return Response<bool>.Fail(ResourceFile.FormUpdateFailed);
                }

                else 
                {
                    return Response<bool>.Fail(ResourceFile.StatusNotFound);
                }
            }
            return Response<bool>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// This method update's the status of the form to cancelled
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>bool</returns>
        public async Task<Response<bool>> SetCancelledStatus(string formId)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);
            if (expenseForm != null)
            {
                var status = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.Cancelled);

                if (status != null)
                {
                    expenseForm.ExpenseStatus = status;
                    bool result = await UpdateDatabase(expenseForm);
                    if (result)
                    {
                        return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
                    }
                    return Response<bool>.Fail(ResourceFile.FormUpdateFailed);
                }

                else
                {
                    return Response<bool>.Fail(ResourceFile.StatusNotFound);
                }
            }
            return Response<bool>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// This method update's the status of the form to disbursed
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>bool</returns>
        public async Task<Response<bool>> SetDisbursedStatus(string formId)
        {
            var response = new Response<bool>();

            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);
            if (expenseForm != null)
            {
                var status = await _unitOfWork.ExpenseStatus.
                    GetExpenseStatusByDescription(FormStatus.Disbursed);
                if(status != null) 
                {
                    expenseForm.ExpenseStatus = status;
                    bool result = await UpdateDatabase(expenseForm);
                    if (result)
                    {
                        return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
                    }
                    else
                    {
                        return Response<bool>.Fail(ResourceFile.FormUpdateFailed);
                    }
                }
                return Response<bool>.Fail(ResourceFile.StatusNotFound);
                               
            }
            return Response<bool>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// This method update's the status of the form to further info required
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>bool</returns>
        public async Task<Response<bool>> SetFurtherInfoRequiredStatus(string formId, string approverNote)
        {
            var response = new Response<bool>();

            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);
            if (expenseForm != null)
            {
                var status = await _unitOfWork.ExpenseStatus.
                    GetExpenseStatusByDescription(FormStatus.FurtherInfoRequired);

                if(status != null) 
                {
                    expenseForm.ExpenseStatus = status;
                    expenseForm.ApproverNote = approverNote;
                    bool result = await UpdateDatabase(expenseForm);
                    if (result)
                    {
                        return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
                    }
                    else
                    {
                        return Response<bool>.Fail(ResourceFile.FormUpdateFailed);
                    }
                }

                return Response<bool>.Fail(ResourceFile.StatusNotFound);
            }
            return Response<bool>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// This method update's the status of the form to new request
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>bool</returns>
        public async Task<Response<bool>> SetNewRequestStatus(string formId)
        {
            var response = new Response<bool>();

            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);
           
            if (expenseForm != null)
            {
                var status = await _unitOfWork.ExpenseStatus.
                    GetExpenseStatusByDescription(FormStatus.NewRequest);

                if (status != null) 
                {
                    expenseForm.ExpenseStatus = status;
                    bool result = await UpdateDatabase(expenseForm);
                    if (result)
                    {
                        return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
                    }
                    else
                    {
                        return Response<bool>.Fail(ResourceFile.FormUpdateFailed);
                    }
                }
                return Response<bool>.Fail(ResourceFile.StatusNotFound);
            }

            return Response<bool>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// This method update's the status of the form to pending approval
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>bool</returns>
        public async Task<Response<bool>> SetPendingApprovalStatus(string formId)
        {
            var response = new Response<bool>();

            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);

            if (expenseForm != null)
            {
                var status = await _unitOfWork.ExpenseStatus.
                    GetExpenseStatusByDescription(FormStatus.PendingApproval);

                if (status != null)
                {
                    expenseForm.ExpenseStatus = status;
                    
                    bool result = await UpdateDatabase(expenseForm);
                    if (result)
                    {
                        return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
                    }
                    else
                    {
                        return Response<bool>.Fail(ResourceFile.FormUpdateFailed);
                    }
                }
                return Response<bool>.Fail(ResourceFile.StatusNotFound);
            }

            return Response<bool>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// This method update's the status of the form to rejected
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>bool</returns>
        public async Task<Response<bool>> SetRejectedStatus(string formId, string approverNote)
        {
            var response = new Response<bool>();

            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);

            if (expenseForm != null)
            {
                var status = await _unitOfWork.ExpenseStatus.
                    GetExpenseStatusByDescription(FormStatus.Rejected);

                if (status != null)
                {
                    expenseForm.ExpenseStatus = status;
                    expenseForm.ApproverNote = approverNote;
                    bool result = await UpdateDatabase(expenseForm);
                    if (result)
                    {
                        return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
                    }
                    else
                    {
                        return Response<bool>.Fail(ResourceFile.FormUpdateFailed);
                    }
                }
                return Response<bool>.Fail(ResourceFile.StatusNotFound);
            }

            return Response<bool>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// This method update's the status of the form to "to be submitted"
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>bool</returns>
        public async Task<Response<bool>> SetToBeSubmitted(string formId)
        {
            var response = new Response<bool>();

            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);

            if (expenseForm != null)
            {
                var status = await _unitOfWork.ExpenseStatus.
                    GetExpenseStatusByDescription(FormStatus.ToBeSubmitted);

                if (status != null)
                {
                    expenseForm.ExpenseStatus = status;
                    bool result = await UpdateDatabase(expenseForm);
                    if (result)
                    {
                        return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
                    }
                    else
                    {
                        return Response<bool>.Fail(ResourceFile.FormUpdateFailed);
                    }
                }
                return Response<bool>.Fail(ResourceFile.StatusNotFound);
            }

            return Response<bool>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// This method updates the DB with the changes made to the update column in the database
        /// </summary>
        /// <returns>int</returns>
        private async Task<bool> UpdateDatabase(ExpenseForm expenseForm)
        {
            try
            {
                _unitOfWork.ExpenseForm.Update(expenseForm);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

                    


