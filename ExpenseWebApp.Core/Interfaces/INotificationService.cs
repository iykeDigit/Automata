using ExpenseWebApp.Dtos.NotificationDtos;
using ExpenseWebApp.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface INotificationService
    {
        Task<Response<IEnumerable<NotificationDto>>> GetApproverNotifications(string cacNumber);
        Task<Response<IEnumerable<NotificationDto>>> GetDisburserNotifications(string cacNumber);
        Task<Response<IEnumerable<NotificationDto>>> GetFormCreatorNotifications(int userId, string cacNumber);
        Task SendNotificationToFormCreator(NotificationCreateDto notificationCreateDto, string cacNumber, string token = null);
        Task SendNotificationToDisburser(NotificationCreateDto notificationCreateDto, string cacNumber, string token = null);
        Task SendNotificationToApprover(NotificationCreateDto notificationCreateDto, string cacNumber, string token = null);
    }
}
