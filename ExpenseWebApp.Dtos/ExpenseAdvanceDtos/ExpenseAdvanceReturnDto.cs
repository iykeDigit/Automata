using ExpenseWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.ExpenseAdvanceDtos
{
    public class ExpenseAdvanceReturnDto
    {
        public string AdvanceFormId { get; set; }
        public string AdvanceFormNo { get; set; }
        public string AdvanceDescription { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string AdvancePurpose { get; set; }
        public string AdvanceNote { get; set; }
        public DateTime AdvanceDate { get; set; }
        public string Attachments { get; set; }
        public string ApprovedBy { get; set; }
        public int CompanyId { get; set; }
        public string ExpenseStatusId { get; set; }
        public ExpenseStatus ExpenseStatus { get; set; }
    }
}