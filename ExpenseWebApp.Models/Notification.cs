using System;

namespace ExpenseWebApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string FormNo { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public bool IsRead { get; set; }
        public bool IsActive { get; set; }
        public DateTime TimeStaamp { get; set; }
        public string ExpenseFormNo { get; set; }
        public string FormStatus { get; set; }
    }
}
