using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Dtos.NotificationDtos;
using ExpenseWebApp.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [HttpGet("Approver-Notifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IEnumerable<NotificationDto>>>> GetApproverNotifications(string cacNumber)
        {
           var result = await _notificationService.GetApproverNotifications(cacNumber);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Disburser-Notifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IEnumerable<NotificationDto>>>> GetDisburserNotifications(string cacNumber)
        {
            var result = await _notificationService.GetDisburserNotifications(cacNumber);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("FormCreator-Notifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IEnumerable<NotificationDto>>>> GetFormCreatorNotifications(int userId, string cacNumber)
        {
            var result = await _notificationService.GetFormCreatorNotifications(userId, cacNumber);
            return StatusCode(result.StatusCode, result);
        }
    }
}
