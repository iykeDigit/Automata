using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.NotificationDtos
{
    public class NotificationCreateDto
    {
        public string FormNo { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public bool IsRead { get; set; }
        public string FormStatus { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
