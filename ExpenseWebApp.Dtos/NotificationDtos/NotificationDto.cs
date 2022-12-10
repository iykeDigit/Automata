using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.NotificationDtos
{
    public class NotificationDto
    {
        public string FormNo { get; set; }
        public bool IsRead { get; set; }
        public bool IsActive { get; set; } = true;
        public string FormStatus { get; set; }
    }
}
