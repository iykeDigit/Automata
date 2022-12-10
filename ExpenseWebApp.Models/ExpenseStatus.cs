using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Models
{
    public class ExpenseStatus
    {
        [Key]
        public string ExpenseStatusId { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
    }
}
