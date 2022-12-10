using ExpenseWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.ExpenseAdvanceDtos
{
    public class ExpenseAdvanceFormResponseDTO
    {
        public string AdvanceFormId { get; set; }
        public string AdvanceFormNo { get; set; }
        public string AdvanceDescription { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string AdvancePurpose { get; set; }
        public DateTime AdvanceDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DisbursementDate { get; set; }
        public string DisbursedBy { get; set; }
        public string Attachments { get; set; }
        public string PaidFrom { get; set; }
        public string ExpenseStatus { get; set; }
       





    }
}
