using AutoMapper;
using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Dtos;
using ExpenseWebApp.Dtos.ExpenseFormDetailsDtos;
using ExpenseWebApp.Dtos.ExpenseFormDto;
using ExpenseWebApp.Dtos.NotificationDtos;
using ExpenseWebApp.Models;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.Helpers;
using ExpenseWebApp.Utilities.Pagination;
using ExpenseWebApp.Utilities.ResourceFiles;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ExpenseWebApp.Core.Implementation
{
    public class ExpenseFormService : IExpenseFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUpdateFormStatus _updateFormStatus;
        private readonly INotificationService _notificationService;
        private readonly ILogger<ExpenseFormService> _logger;
        private readonly ICompanyService _companyService;
        private readonly IFormNumberGeneratorService _formNumberGeneratorService;
        static object _lock = new object();

        public ExpenseFormService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IUpdateFormStatus updateFormStatus,
            INotificationService notificationService,
            ICompanyService companyService,
            IFormNumberGeneratorService formNumberGeneratorService,
            ILogger<ExpenseFormService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _updateFormStatus = updateFormStatus;
            _notificationService = notificationService;
            _logger = logger;
            _companyService = companyService;
            _formNumberGeneratorService = formNumberGeneratorService;
        }






        /// <summary>
        /// Gets a single expense form by id
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>An expense form with its expense form details</returns>
        public async Task<Response<ExpenseFormResponseDto>> GetExpenseFormById(string formId, string cacNumber)
        {

            var form = await _unitOfWork.ExpenseForm.GetExpenseFormById(formId);
            var userCompany = await _companyService.GetCompanyUsers(cacNumber, null);
            var user = userCompany.UserInfo.Where(user => user.Id == form.UserId).FirstOrDefault();

            if (form == null || user == null)
            {
                return Response<ExpenseFormResponseDto>.Fail("Record Not Found");
            }

            var result = _mapper.Map<ExpenseFormResponseDto>(form);

            result.EmployeeName = user.FullName;

            return Response<ExpenseFormResponseDto>.Success("Expense form with this id", result);

        }

        /// <summary>
        /// Fetches all approved forms
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="cacNumber"></param>
        /// <returns>A paginated result of IEnumerable<ExpenseFormResponseDto></returns>
        public async Task<Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>> GetAllApprovedForms(PagingDto paging, string cacNumber)
        {
            var company = await _companyService.GetCompany(cacNumber, null);
            if (company == null)
            {
                return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Fail(ResourceFile.Unsuccessful);
            }

            var expenseForms = _unitOfWork.ExpenseForm.GetExpenseFormsByCompanyId(company.CompanyId)
                                .Where(q => q.ExpenseStatus.Description == FormStatus.Approved)
                                .OrderByDescending(x => x.DateCreated);

            var paginatedExpenseForms = await expenseForms.PaginateAsync<ExpenseForm, ExpenseFormResponseDto>(paging.PageSize, paging.PageNumber, _mapper);
            return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Success(ResourceFile.Success, paginatedExpenseForms);
        }

        /// <summary>
        /// This Method updates expense form detail
        /// </summary>
        /// <param name="formDetailId">form detail id</param>
        /// <param name="expenseFormDto"></param>
        /// <returns>a boolean</returns>
        public async Task<Response<bool>> EditExpenseFormDetail(string formDetailId, ExpenseFormDetailDto expenseFormDto)
        {
            var expenseDetail = await _unitOfWork.ExpenseFormDetails.GetExpenseFormDetailsById(formDetailId);

            if (expenseDetail != null && expenseDetail.ExpenseForm.ExpenseStatus.Description == FormStatus.NewRequest)
            {
                var expenseForm = _mapper.Map(expenseFormDto, expenseDetail);
                _unitOfWork.ExpenseFormDetails.Update(expenseForm);
                await _unitOfWork.SaveAsync();

                return Response<bool>.Success(ResourceFile.FormUpdateSuccess, true);
            }
            return Response<bool>.Fail(ResourceFile.FormNotFound, StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// This method save an expense form to the database and
        /// also update the status to 'ToBeSubmitted'
        /// </summary>
        /// <param name="formId">expense form id</param>
        /// <returns>a boolean</returns>
        public async Task<Response<IEnumerable<ExpenseFormDetailResponseDto>>> SaveExpenseForm(string formId, List<ExpenseFormDetailDto> expenses)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseFormById(formId);
            List<ExpenseFormDetails> form = null;

            if (expenseForm != null)
            {
                var expenseForms = _mapper.Map(expenses, form);

                expenseForms.ForEach(x =>
                {
                    var getCategory = _unitOfWork.ExpenseCategory.GetExpenseCategoryByName(x.ExpenseCategory.ExpenseCategoryName);
                    x.ExpenseFormId = formId;
                    x.ExpenseCategoryId = getCategory?.ExpenseCategoryId;
                    x.ExpenseCategory = null;
                });
                _unitOfWork.ExpenseFormDetails.AddRange(expenseForms);
                await _unitOfWork.SaveAsync();
                var expenseDetailForms = await _unitOfWork.ExpenseFormDetails.GetAllExpenseFormDetailsByFormId(formId);
                var result = _mapper.Map<IEnumerable<ExpenseFormDetailResponseDto>>(expenseDetailForms);
                return Response<IEnumerable<ExpenseFormDetailResponseDto>>.Success(ResourceFile.Success, result);
            }
            return Response<IEnumerable<ExpenseFormDetailResponseDto>>.Fail(ResourceFile.FormNotFound, StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// This method submit an expense form to the database with
        /// the status PendingApproval
        /// </summary>
        /// <param name="formId">expense form id</param>
        /// <returns>a boolean</returns>
        public async Task<Response<bool>> SubmitExpenseForm(string formId, string cacNumber, List<ExpenseFormDetailDto> expenses)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseFormById(formId);

            var status = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.PendingApproval);

            List<ExpenseFormDetails> form = new List<ExpenseFormDetails>();

            if (expenseForm != null)
            {
                if (expenses.Any())
                {
                    var expenseForms = _mapper.Map(expenses, form);

                    expenseForms.ForEach(x =>
                    {
                        var getCategory = _unitOfWork.ExpenseCategory.GetExpenseCategoryByName(x.ExpenseCategory.ExpenseCategoryName);
                        x.ExpenseFormId = formId;
                        x.ExpenseCategoryId = getCategory?.ExpenseCategoryId;
                        x.ExpenseCategory = null;
                    });

                    _unitOfWork.ExpenseFormDetails.AddRange(expenseForms);
                    await _unitOfWork.SaveAsync();
                }
                expenseForm.ExpenseStatus = status;
                _unitOfWork.ExpenseForm.UpdateFormStatus(expenseForm);
                var notificationDto = CreateNotificationDto(expenseForm);
                await _notificationService.SendNotificationToApprover(notificationDto, cacNumber);
                return Response<bool>.Success(ResourceFile.Success, true);
            }
            return Response<bool>.Fail(ResourceFile.FormNotFound, StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// A method that gets all forms belongingto an employee
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="userId"></param>
        /// <returns>A paginated list of all employee's forms</returns>
        public async Task<Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>> GetEmployeeExpenseFormsAsync(PagingDto paging, int userId, string cacNumber)
        {
            var expenseForms = _unitOfWork.ExpenseForm.GetAllExpenseForms().Where(x => x.UserId == userId)
                                                      .OrderByDescending(x => x.DateCreated);
            var totalCount = expenseForms.Count();
            var userCompany = await _companyService.GetCompanyUsers(cacNumber, null);
            var user = userCompany.UserInfo.Where(user => user.Id == userId).FirstOrDefault();

            if (user == null)
            {
                return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Fail(ResourceFile.InvalidData);
            }


            if (expenseForms.Any())
            {
                var paginatedExpenseForms = await expenseForms.PaginateAsync<ExpenseForm, ExpenseFormResponseDto>(paging.PageSize, paging.PageNumber, _mapper, totalCount);
                
                paginatedExpenseForms.PageItems.ToList().ForEach(x => x.EmployeeName = user.FullName);
                return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Success(ResourceFile.Success, paginatedExpenseForms);
            }
            return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Success(ResourceFile.FormNotFound, null);
        }

        /// <summary>
        /// Filters a sequence of expense forms based on status payment and approval status.
        /// </summary>
        /// <param name="paging">Page number and page size</param>
        /// <returns>Return paginated expense forms paid by employee</returns>
        public async Task<Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>> GetApprovedExpenseFormsPaidByEmployeeAsync(PagingDto paging)
        {
            var expenseForms = _unitOfWork.ExpenseForm.GetAllExpenseForms().Where(x => x.ExpenseStatus.Description.ToLower().Trim() == FormStatus.Approved.ToLower().Trim()
                                                                            && x.PaidBy.ToLower().Trim() == PaidBy.Employee.ToLower().Trim())
                                                                            .OrderByDescending(x => x.DateCreated); ;

            if (expenseForms.Any())
            {
                var paginatedExpenseForms = await expenseForms.PaginateAsync<ExpenseForm, ExpenseFormResponseDto>(paging.PageSize, paging.PageNumber, _mapper);
                return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Success(ResourceFile.Success, paginatedExpenseForms);
            }
            return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Success(ResourceFile.FormNotFound, null);
        }

        /// <summary>
        /// Filters a sequence of expense-forms based on form statuses; "To Be Submitted" and "NewRequest".
        /// </summary>
        /// <param name="paging">Page number and page size</param>
        /// <returns>Paginated list of all submitted forms</returns>
        public async Task<Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>> GetAllSubmittedExpenseFormsAsync(PagingDto paging)
        {
            var expenseForms = _unitOfWork.ExpenseForm.GetAllExpenseForms().Where(x => x.ExpenseStatus.Description.ToLower().Trim() != FormStatus.ToBeSubmitted.ToLower().Trim()
                                                                                    || x.ExpenseStatus.Description.ToLower().Trim() != FormStatus.NewRequest.ToLower().Trim())
                                                                                    .OrderByDescending(x => x.DateCreated); ;

            if (expenseForms.Any())
            {
                var paginatedExpenseForms = await expenseForms.PaginateAsync<ExpenseForm, ExpenseFormResponseDto>(paging.PageSize, paging.PageNumber, _mapper);
                return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Success(ResourceFile.Success, paginatedExpenseForms);
            }
            return Response<PaginatorHelper<IEnumerable<ExpenseFormResponseDto>>>.Success(ResourceFile.FormNotFound, null);
        }
       
        public async Task<Response<string>> DiscardExpenseFormsAsync(string formId)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseForm(formId);

            if (expenseForm != null && expenseForm.ExpenseFormDetails.Count > 0)
            {
                foreach (var form in expenseForm.ExpenseFormDetails)
                {
                    _unitOfWork.ExpenseFormDetails.Delete(form);
                }
                await UpdateDatabase(expenseForm);

                foreach (var item in expenseForm.ExpenseFormDetails)
                {
                    ExpenseFormDetailsService.DeleteAttachment(item.Attachments);
                }
                
                return Response<string>.Success(ResourceFile.Success, null);
            }
            return Response<string>.Fail(ResourceFile.FormNotFound);
        }




        /// <summary>
        /// Sets the status description of the Expense form to Approved if the form exists.
        /// </summary>
        /// <param name="formNumber"></param>
        /// <returns>Returns an instance of ExpenseFormResponseDto if a form with the provided form number exists
        /// in record, else returns a failed response.
        /// </returns>
        public async Task<Response<ExpenseFormResponseDto>> ApproveExpenseForm(string formId, string cacNumber, string token)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseFormById(formId);
            if (expenseForm != null && expenseForm.ExpenseStatus.Description != FormStatus.Disbursed)
            {
                var updateFormStatus = await _updateFormStatus.SetApprovedStatus(expenseForm.ExpenseFormId);
                if (updateFormStatus.Succeeded)
                {
                    var result = _mapper.Map<ExpenseFormResponseDto>(expenseForm); //Send Email to Requestor
                    var notificationDto = CreateNotificationDto(expenseForm);
                    await _notificationService.SendNotificationToFormCreator(notificationDto, cacNumber, token);
                    return Response<ExpenseFormResponseDto>.Success(ResourceFile.Success, result);
                }
                return Response<ExpenseFormResponseDto>.Fail(updateFormStatus.Message);
            }
            return Response<ExpenseFormResponseDto>.Fail(ResourceFile.Unsuccessful);
        }

        /// <summary>
        /// Sets the status description of the Expense form to Rejected if the form exists.
        /// </summary>
        /// <param name="formNumber"></param>
        /// <returns>Returns an instance of ExpenseFormResponseDto if a form with the provided form number exists
        /// in record, else returns a failed response.
        /// </returns>
        public async Task<Response<ExpenseFormResponseDto>> RejectExpenseForm(string approverNote, string formId, string cacNumber, string token)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseFormById(formId);
            if (expenseForm != null && expenseForm.ExpenseStatus.Description != FormStatus.Disbursed)
            {
                var updateFormStatus = await _updateFormStatus.SetRejectedStatus(expenseForm.ExpenseFormId, approverNote);
                if (updateFormStatus.Succeeded)
                {
                    var result = _mapper.Map<ExpenseFormResponseDto>(expenseForm); //Send Email to Requestor
                    var notificationDto = CreateNotificationDto(expenseForm);
                    await _notificationService.SendNotificationToFormCreator(notificationDto, cacNumber, token);
                    return Response<ExpenseFormResponseDto>.Success(ResourceFile.Success, result);
                }
                return Response<ExpenseFormResponseDto>.Fail(updateFormStatus.Message);
            }

            return Response<ExpenseFormResponseDto>.Fail(ResourceFile.Unsuccessful);

        }

        /// <summary>
        /// Request further info from the employee and updates the status appropriately
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="cacNumber"></param>
        /// <param name="token"></param>
        /// <returns></returns>

        public async Task<Response<ExpenseFormResponseDto>> FurtherInfoRequired(string approverNote, string formId, string cacNumber, string token)

        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseFormById(formId);
            if (expenseForm != null)
            {
                var updateFormStatus = await _updateFormStatus.SetFurtherInfoRequiredStatus(expenseForm.ExpenseFormId, approverNote);
                if (updateFormStatus.Succeeded)
                {
                    var status = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.FurtherInfoRequired);
                    expenseForm.ExpenseStatus = status;
                    var result = _mapper.Map<ExpenseFormResponseDto>(expenseForm);

                    var notificationDto = CreateNotificationDto(expenseForm);

                    await _notificationService.SendNotificationToFormCreator(notificationDto, cacNumber, token);
                    return Response<ExpenseFormResponseDto>.Success(ResourceFile.Success, result);
                }
            }

            return Response<ExpenseFormResponseDto>.Fail(ResourceFile.Unsuccessful);
        }

        /// <summary>
        /// Reimburses the Expense and sets the status of the expense form to Disbursed
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="cacNumber"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Response<ExpenseFormResponseDto>> ReimburseExpense(string formId, string cacNumber, string token)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseFormById(formId);
            if (expenseForm != null && expenseForm.ExpenseStatus.Description == FormStatus.Approved)
            {
                var updateFormStatus = await _updateFormStatus.SetDisbursedStatus(expenseForm.ExpenseFormId);

                if (updateFormStatus.Succeeded)
                {
                    var status = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.Disbursed);
                    expenseForm.ExpenseStatus = status;
                    var result = _mapper.Map<ExpenseFormResponseDto>(expenseForm);

                    var notificationDto = CreateNotificationDto(expenseForm);

                    await _notificationService.SendNotificationToFormCreator(notificationDto, cacNumber, token);
                    await _notificationService.SendNotificationToDisburser(notificationDto, cacNumber, token);
                    return Response<ExpenseFormResponseDto>.Success(ResourceFile.Success, result);
                }
                else
                {
                    return Response<ExpenseFormResponseDto>.Fail(ResourceFile.FormUpdateFailed);
                }
            }

            return Response<ExpenseFormResponseDto>.Fail(ResourceFile.Unsuccessful);
        }

        /// <summary>
        /// Delete the Expense Form from the DB
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<Response<ExpenseFormResponseDto>> CancelExpenseForm(string formId)
        {
            var expenseForm = await _unitOfWork.ExpenseForm.GetExpenseFormById(formId);

            if (expenseForm != null)
            {
                var updateFormStatus = await _updateFormStatus.SetCancelledStatus(expenseForm.ExpenseFormId);

                if (updateFormStatus.Succeeded)
                {
                    var status = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.Cancelled);
                    expenseForm.ExpenseStatus = status;
                    var result = _mapper.Map<ExpenseFormResponseDto>(expenseForm);
                    return Response<ExpenseFormResponseDto>.Success(ResourceFile.Success, result);
                }
                return Response<ExpenseFormResponseDto>.Fail(ResourceFile.FormUpdateFailed);
            }
            return Response<ExpenseFormResponseDto>.Fail(ResourceFile.FormNotFound);
        }

        /// <summary>
        /// Creates a new instance of ExpenseForm
        /// </summary>
        /// <param name="expenseFormCreateDto"></param>
        /// <param name="cacNumber"></param>
        /// <returns></returns>
        public async Task<Response<ExpenseFormCreateResponseDto>> CreateExpenseForm(ExpenseFormCreateRequestDto expenseFormCreateDto, string cacNumber)
        {
            _logger.LogInformation("Attempting to fetch user company id");
            var companyResponse = await _companyService.GetCompany(cacNumber, expenseFormCreateDto.Token);
            var userCompany = await _companyService.GetCompanyUsers(cacNumber, null);
            var user = userCompany.UserInfo.Where(user => user.Id == expenseFormCreateDto.UserId).FirstOrDefault();

            var expenseForm = _mapper.Map<ExpenseForm>(expenseFormCreateDto);
            expenseForm.DateCreated = DateTime.Now;

            if (expenseForm != null && companyResponse != null)
            {
                _logger.LogInformation("user company details obtained successfully");
                expenseForm.CompanyId = companyResponse.CompanyId;

                lock (_lock)
                {
                    expenseForm.ExpenseFormNo = _formNumberGeneratorService.GenerateFormNumber(ResourceFile.ExpenseForm, cacNumber);
                }

               


                var status = await _unitOfWork.ExpenseStatus.GetExpenseStatusByDescription(FormStatus.NewRequest);

                if (status != null)
                {
                    expenseForm.ExpenseStatusId = status.ExpenseStatusId;

                    await _unitOfWork.ExpenseForm.AddAsync(expenseForm);
                    await _unitOfWork.SaveAsync();

                    var result = _mapper.Map<ExpenseFormCreateResponseDto>(expenseForm);
                    result.EmployeeName = user.FullName;
                    return Response<ExpenseFormCreateResponseDto>.Success(ResourceFile.Success, result);
                }

                _logger.LogInformation("Form status not found");
                return Response<ExpenseFormCreateResponseDto>.Fail(ResourceFile.Unsuccessful);
            }

            _logger.LogInformation("Operation failed");

            return Response<ExpenseFormCreateResponseDto>.Fail(ResourceFile.Unsuccessful);

        }

       
        /// <summary>
        /// Creates an instance of NotificationCreateDto
        /// </summary>
        /// <param name="expenseForm"></param>
        /// <returns></returns>
        private NotificationCreateDto CreateNotificationDto(ExpenseForm expenseForm)
        {
            return new NotificationCreateDto()
            {
                FormNo = expenseForm.ExpenseFormNo,
                CompanyId = expenseForm.CompanyId,
                FormStatus = expenseForm.ExpenseStatus.Description,
                IsRead = false,
                UserId = expenseForm.UserId
            };
        }

        

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