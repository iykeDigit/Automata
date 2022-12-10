using AutoMapper;
using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Dtos;
using ExpenseWebApp.Dtos.ExpenseAdvanceDtos;
using ExpenseWebApp.Models;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.Pagination;
using ExpenseWebApp.Utilities.ResourceFiles;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class ExpenseAdvanceService : IExpenseAdvance
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly IFormNumberGeneratorService _formNumberGeneratorService;
        static object _lock = new object();


        public ExpenseAdvanceService(IMapper mapper, IUnitOfWork unitOfWork, ICompanyService companyService, IFormNumberGeneratorService formNumberGeneratorService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _companyService = companyService;
            _formNumberGeneratorService = formNumberGeneratorService;
    }

        /// <inheritdoc />
        public async Task<Response<string>> CreateCashAdvance(CreateExpenseAdvanceDto advanceDto)
        {

            var companyResponse = await _companyService.GetCompany(advanceDto.CACNumber, advanceDto.Token);

            if (companyResponse == null) return Response<string>.Fail(ResourceFile.Unsuccessful);

            var expenseAdvance = _mapper.Map<ExpenseAdvance>(advanceDto);
            expenseAdvance.DateCreated = DateTime.Now;
            expenseAdvance.CompanyId = companyResponse.CompanyId;
            expenseAdvance.AdvanceFormNo = _formNumberGeneratorService.GenerateFormNumber(ResourceFile.CashAdvanceForm, advanceDto.CACNumber);


            if (advanceDto.AdvanceAmount <= 0)
            {
                return Response<string>.Fail(ResourceFile.InvalidAmount, 400);
            }

            var expenseAvance = _mapper.Map<ExpenseAdvance>(advanceDto);
            var formStatus = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.ToBeSubmitted);
            if (formStatus == null) return Response<string>.Fail(ResourceFile.Unsuccessful);

            expenseAdvance.ExpenseStatusId = formStatus.ExpenseStatusId;

            await _unitOfWork.ExpenseAdvance.AddAsync(expenseAdvance);
            await _unitOfWork.SaveAsync();

            return Response<string>.Success(ResourceFile.Success, ResourceFile.Success);

            
        }

        /// <inheritdoc />
        public async Task<Response<PaginatorHelper<IEnumerable<ExpenseAdvanceReturnDto>>>> GetPendingRequestsAsync(PagingDto paging, int companyId)
        {
            var query = _unitOfWork.ExpenseAdvance.GetAllExpenseAdvanceForms();
            query = query.Where(f => f.ExpenseStatus.Description == FormStatus.PendingApproval && f.CompanyId == companyId);

            var result = await Paginator.PaginateAsync<ExpenseAdvance, ExpenseAdvanceReturnDto>
                                                                (query, paging.PageSize, paging.PageNumber, _mapper, query.Count());
            return Response<PaginatorHelper<IEnumerable<ExpenseAdvanceReturnDto>>>.Success(ResourceFile.Success, result);
        }

        /// <inheritdoc/>
        public async Task<Response<string>> SubmitAdvanceRequest(SubmitExpenseAdvanceDto expenseAdvanceDto)
        {
            if (0 >= expenseAdvanceDto.AdvanceAmount)
            {
                return Response<string>.Fail(ResourceFile.InvalidAmount, 400);
            }

            var expenseAdvance = _mapper.Map<ExpenseAdvance>(expenseAdvanceDto);
            var formStatus = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.PendingApproval);
            expenseAdvance.ExpenseStatusId = formStatus.ExpenseStatusId;

            await _unitOfWork.ExpenseAdvance.AddAsync(expenseAdvance);
            await _unitOfWork.SaveAsync();

            return Response<string>.Success(ResourceFile.Success, ResourceFile.Success);
        }

        ///<inheritdoc/>
        public async Task<Response<bool>> UpdateAdvanceRequestForApproverAsync(string formId, UpdateExpenseAdvanceDto updateExpense)
        {
            var expense = await _unitOfWork.ExpenseAdvance.GetExpenseAdvanceById(formId);
            var (isValid, message) = UpdateValidation(expense, updateExpense);
            var response = new Response<bool> { Message = message };

            if (isValid != false)
            {
                var status = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(updateExpense.ExpenseStatusDescription);
                expense.ApproverNote = updateExpense.ApproverNote;
                expense.ExpenseStatusId = status.ExpenseStatusId;
                expense.ExpenseStatus = status;
                expense.ApprovedBy = updateExpense.ApprovedBy;

                _unitOfWork.ExpenseAdvance.Update(expense);
                await _unitOfWork.SaveAsync();

                response.Data = true;
                response.Succeeded = true;
                response.StatusCode = 200;
                return response;
            }
            response.StatusCode = 400;
            return response;
        }

        private static (bool, string) UpdateValidation(ExpenseAdvance expense, UpdateExpenseAdvanceDto updateExpense)
        {
            if (expense == null)
            {
                return (false, ResourceFile.FormNotFound);
            }

            if (expense.ExpenseStatus.Description == FormStatus.Approved)
            {
                return (false, ResourceFile.ProcessedForm);
            }

            if (updateExpense.ExpenseStatusDescription != FormStatus.Approved &&
                updateExpense.ExpenseStatusDescription != FormStatus.Rejected &&
                updateExpense.ExpenseStatusDescription != FormStatus.FurtherInfoRequired)
            {
                return (false, ResourceFile.InvalidStatus);
            }

            if (updateExpense.ExpenseStatusDescription == FormStatus.FurtherInfoRequired && string.IsNullOrWhiteSpace(updateExpense.ApproverNote))
            {
                return (false, ResourceFile.FurtherInfoNoteRequired);
            }

            if (updateExpense.ExpenseStatusDescription == FormStatus.Rejected && string.IsNullOrWhiteSpace(updateExpense.ApproverNote))
            {
                return (false, ResourceFile.RejectionNoteRequired);
            }

            return (true, string.Empty);
        }


        /// <summary>
        /// Get All Approved Expense Advance Forms
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public async Task<Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>> GetApprovedCashAdvanceExpenseForms(PagingDto paging) 
        {
           var advanceExpenseForms = _unitOfWork.ExpenseAdvance.GetAllExpenseAdvanceForms()
                                .Where(q => q.ExpenseStatus.Description == FormStatus.Approved)
                                .OrderByDescending(x => x.AdvanceDate);
            

            if(advanceExpenseForms == null) 
            {
                return Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>.Fail(ResourceFile.Unsuccessful);
            }

            var totalCount = advanceExpenseForms.Count();

            var paginatedExpenseForms = await advanceExpenseForms.PaginateAsync<ExpenseAdvance, ExpenseAdvanceFormResponseDTO>(paging.PageSize, paging.PageNumber, _mapper, totalCount);
            return Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>.Success(ResourceFile.Success, paginatedExpenseForms);
        }

        /// <summary>
        /// Get All Requestor's Expense Advance Forms
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="userId"></param>
        /// <param name="cacNumber"></param>
        /// <returns></returns>
        public async Task<Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>> GetRequestorExpenseAdvanceForms(PagingDto paging, int userId, string cacNumber) 
        {
            var advanceExpenseForms = _unitOfWork.ExpenseAdvance.GetAllExpenseAdvanceForms().Where(x => x.UserId == userId)
                                                      .OrderByDescending(x => x.AdvanceDate);
            
            if (advanceExpenseForms == null)
            {
                return Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>.Fail(ResourceFile.UserFormNotFound);
            }

            else if (advanceExpenseForms.Any())
            {
                var totalCount = advanceExpenseForms.Count();
                var paginatedExpenseAdvanceForms = await advanceExpenseForms.PaginateAsync<ExpenseAdvance, ExpenseAdvanceFormResponseDTO>(paging.PageSize, paging.PageNumber, _mapper, totalCount);
                return Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>.Success(ResourceFile.Success, paginatedExpenseAdvanceForms);
            }

            return Response<PaginatorHelper<IEnumerable<ExpenseAdvanceFormResponseDTO>>>.Success(ResourceFile.FormNotFound, null);
        }

        /// <summary>
        /// Edit cash advance form to be submitted for approval
        /// </summary>
        /// <param name="editAdvanceDto"></param>
        /// <returns>Form successfully edited</returns>
        public async Task<Response<bool>> EditCashAdvance(string formDetailId, EditExpenseAdvanceDto editAdvanceDto)
        {
            var expenseAdvance = await _unitOfWork.ExpenseAdvance.GetExpenseAdvanceById(formDetailId);
            var formStatus = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.ToBeSubmitted);
            
            if (expenseAdvance != null && expenseAdvance.ExpenseStatusId == formStatus.ExpenseStatusId)
            {
                var expenseForm = _mapper.Map(editAdvanceDto, expenseAdvance);
                _unitOfWork.ExpenseAdvance.Update(expenseForm);
                await _unitOfWork.SaveAsync();
                return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
            }

            return Response<bool>.Fail(ResourceFile.FormUpdateFailed, StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// delete cash advance form to be submitted for approval
        /// </summary>
        /// <param name="editAdvanceDto"></param>
        /// <returns>Form successfully edited</returns>
        public async Task<Response<string>> DiscardFormsAsync(string formId)
        {
            var expenseAdvance = await _unitOfWork.ExpenseAdvance.GetExpenseAdvanceById(formId);
            if (expenseAdvance == null) return Response<string>.Fail(ResourceFile.FormNotFound);
            
            var formStatus = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.ToBeSubmitted);

            if (expenseAdvance.ExpenseStatusId == formStatus.ExpenseStatusId)
            {
                _unitOfWork.ExpenseAdvance.Delete(expenseAdvance);
                await _unitOfWork.SaveAsync();
                return Response<string>.Success(ResourceFile.Success, null);
            }
            return Response<string>.Fail(ResourceFile.Unsuccessful);
        }
    }
}