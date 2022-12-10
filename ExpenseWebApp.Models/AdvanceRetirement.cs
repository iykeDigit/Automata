using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Models
{
    public class AdvanceRetirement
    {
        [Key]
        public string AdvanceRetirementId { get; set; } = Guid.NewGuid().ToString();
        public string AdvanceRetirementFormNo { get; set; }
        public decimal RetiredAmountDiff { get; set; }
        public int UserId { get; set; }       
        public int CompanyId { get; set; }
        public string PaidBy { get; set; }
        public string ApproverNote { get; set; }
        public string ExpenseStatusId { get; set; }
        public string PaidFromId { get; set; }
        public string ExpenseFormId { get; set; }
        public string Attachments { get; set; }
        public string AdvanceFormId { get; set; }

        // Navigation Properties
        public ExpenseStatus ExpenseStatus { get; set; }
        public PaidFrom PaidFrom { get; set; }
        public ExpenseForm ExpenseForm { get; set; }
        public ExpenseAdvance AdvanceForm { get; set; }

        

    }
}
