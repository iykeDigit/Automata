using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Models
{
    public class PaidFrom
    {
        [Key]
        public string PaidFromId { get; set; } = Guid.NewGuid().ToString();
        public string PaidFromName { get; set; }
        public ICollection<AdvanceRetirement> AdvanceRetirement { get; set; }
        public ICollection<ExpenseAdvance> ExpenseAdvance { get; set; }
    }
}
