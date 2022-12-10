using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Models
{
    public class ExpenseAdvance
    {
        [Key]
        public string AdvanceFormId { get; set; } = Guid.NewGuid().ToString();
        public string AdvanceFormNo { get; set; }
        public string AdvanceDescription { get; set; }             
        public decimal AdvanceAmount { get; set; }
        public string AdvancePurpose { get; set; }
        public string AdvanceNote { get; set; }
        public DateTime AdvanceDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ApproverNote { get; set; }
        public DateTime? DisbursementDate { get; set; }
        public string DisbursedBy { get; set; }
        public string Attachments { get; set; }
        [Required]
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string PaidFromId { get; set; }     
        [Required]
        public string ExpenseStatusId { get; set; }
        public DateTime DateCreated { get; set; }

        // FOREIGN KEYS
        public PaidFrom PaidFrom { get; set; }
        public ExpenseStatus ExpenseStatus { get; set; }
        public ICollection<AdvanceRetirement> AdvanceRetirement { get; set; }
    }
}
