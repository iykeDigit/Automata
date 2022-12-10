using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos
{
    public class SubmitExpenseAdvanceDto
    {
        public string AdvanceDescription { get; set; }
        public int UserId { get; set; }
        public string AdvanceFormNumber { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string AdvancePurpose { get; set; }
        public string AdvanceNote { get; set; }
        public DateTime AdvanceDate { get; set; }
        public int CompanyId { get; set; }
        public string Employee { get; set; }

    }
}
