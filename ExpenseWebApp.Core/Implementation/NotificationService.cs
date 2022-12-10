using AutoMapper;
using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Dtos.NotificationDtos;
using ExpenseWebApp.Models;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.ResourceFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<NotificationService> logger, ICompanyService companyService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _companyService = companyService;
        }

        /// <summary>
        /// Retrieves all notifications where form status is "New Request" and IsRead property false
        /// </summary>
        /// <param name="companyId">Id of the requesting company</param>
        /// <returns>Returns an IEnumerable of NotificationDto</returns>
        public async Task<Response<IEnumerable<NotificationDto>>> GetApproverNotifications(string cacNumber)
        {
            var company = await _companyService.GetCompany(cacNumber, null);
            if(company == null)
            {
                return Response<IEnumerable<NotificationDto>>.Fail(ResourceFile.Unsuccessful);
            }
            var notifications = await _unitOfWork.Notifications.GetAllByCompanyId(company.CompanyId);
            var approverNotifications = notifications.Where(q => q.FormStatus.ToLower() == FormStatus.PendingApproval.ToLower() && !q.IsRead);
            var result = _mapper.Map<IEnumerable<NotificationDto>>(approverNotifications);
            return Response<IEnumerable<NotificationDto>>.Success(ResourceFile.Success, result);
        }


        /// <summary>
        /// Retrieves all notifications where form status is "Approved" and IsRead property false
        /// </summary>
        /// <param name="companyId">Id of the requesting company</param>
        /// <returns>Returns an IEnumerable of NotificationDto</returns>
        public async Task<Response<IEnumerable<NotificationDto>>> GetDisburserNotifications(string cacNumber)
        {
            var company = await _companyService.GetCompany(cacNumber, null);
            if (company == null)
            {
                return Response<IEnumerable<NotificationDto>>.Fail(ResourceFile.Unsuccessful);
            }
            var notifications = await _unitOfWork.Notifications.GetAllByCompanyId(company.CompanyId);
            var disburserNotifications = notifications.Where(q => q.FormStatus.ToLower() == FormStatus.Approved.ToLower() && !q.IsRead);
            var result = _mapper.Map<IEnumerable<NotificationDto>>(disburserNotifications);
            return Response<IEnumerable<NotificationDto>>.Success(ResourceFile.Success, result);
        }


        /// <summary>
        /// Sends email notification to all disbursers
        /// </summary>
        /// <param name="notificationCreateDto"></param>
        /// <param name="cacNumber"></param>
        /// <returns></returns>
        public async Task SendNotificationToDisburser(NotificationCreateDto notificationCreateDto, string cacNumber, string token)
        {
            var users = await _companyService.GetCompanyUsers(cacNumber, token);
            
            if(users == null)
            {
                _logger.LogInformation(ResourceFile.UsersNotFound);
            }
            var disbursers = users.UserInfo.Where(q => q.UserType == UserRoles.Disburser);

            //Create notification and add to db
            var company = await _companyService.GetCompany(cacNumber, null);
            notificationCreateDto.CompanyId = company.CompanyId;
            await CreateNewNotification(notificationCreateDto);            

        }


        /// <summary>
        /// Sends email notification to the requestor
        /// </summary>
        /// <param name="notificationCreateDto"></param>
        /// <param name="cacNumber">unique company's CAC Number</param>
        /// <returns>Returns a NotificationDto</returns>
        public async Task SendNotificationToFormCreator(NotificationCreateDto notificationCreateDto, string cacNumber, string token)
        {
            var users = await _companyService.GetCompanyUsers(cacNumber, token);
            var requestor = users.UserInfo.SingleOrDefault(q => q.Id == notificationCreateDto.UserId);

            //Create notification and add to db
            var company = await _companyService.GetCompany(cacNumber, null);
            notificationCreateDto.CompanyId = company.CompanyId;
            await CreateNewNotification(notificationCreateDto);

        }


        /// <summary>
        /// Sends email notification to all approvers
        /// </summary>
        /// <param name="notificationCreateDto"></param>
        /// <param name="cacNumber"></param>
        /// <returns></returns>
        public async Task SendNotificationToApprover(NotificationCreateDto notificationCreateDto, string cacNumber, string token = null)
        {
            var users = await _companyService.GetCompanyUsers(cacNumber);

            if (users == null)
            {
                _logger.LogInformation(ResourceFile.UsersNotFound);
            }
            var approvers = users.UserInfo.Where(q => q.UserType.ToLower() == UserRoles.Approver.ToLower());

            //Create notification and add to db
            var company = await _companyService.GetCompany(cacNumber);
            notificationCreateDto.CompanyId = company.CompanyId;
            await CreateNewNotification(notificationCreateDto);

        }


        /// <summary>
        /// Creates a new notification and saves to database
        /// </summary>
        /// <param name="notificationCreateDto"></param>
        /// <returns>NotificationDto</returns>
        private async Task CreateNewNotification(NotificationCreateDto notificationCreateDto)
        {
            var newNotification = _mapper.Map<Notification>(notificationCreateDto);
            try
            {
                await _unitOfWork.Notifications.AddAsync(newNotification);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("Notification sent successfully");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Notification failed to send", e.Message);
                return ;                
            }         
        }

        public async Task<Response<IEnumerable<NotificationDto>>> GetFormCreatorNotifications(int userId, string cacNumber)
        {
            var users = await _companyService.GetCompany(cacNumber, null);
            if(users == null)
            {
                return Response<IEnumerable<NotificationDto>>.Fail(ResourceFile.NotExisting);
            }

            var notifications =  await _unitOfWork.Notifications.GetAll().Where(x => x.UserId == userId && x.CompanyId == users.CompanyId).ToListAsync();

            var userNotifications = notifications.Where(q => (q.FormStatus.ToLower() == FormStatus.Approved.ToLower() && !q.IsRead) 
                                                        || (q.FormStatus.ToLower() == FormStatus.Rejected.ToLower() && !q.IsRead)
                                                        || (q.FormStatus.ToLower() == FormStatus.FurtherInfoRequired.ToLower() && !q.IsRead));
            var result = _mapper.Map<IEnumerable<NotificationDto>>(userNotifications);
            return Response<IEnumerable<NotificationDto>>.Success(ResourceFile.Success, result);
        }
    }
}
