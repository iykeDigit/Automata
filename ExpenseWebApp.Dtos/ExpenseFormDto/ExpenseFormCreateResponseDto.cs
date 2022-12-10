using ExpenseWebApp.Dtos.ExpenseFormDetailsDtos;
using System;
using System.Collections.Generic;

namespace ExpenseWebApp.Dtos.ExpenseFormDto
{
    public class ExpenseFormCreateResponseDto
    {
        public string ExpenseFormId { get; set; }
        public string ExpenseFormNo { get; set; }
        public string Description { get; set; }
        public decimal ReimburseableAmount { get; set; }
        public DateTime ReimbursementDate { get; set; }
        public DateTime DateCreated { get; set; }
        public string PaidBy { get; set; }
        public string ApprovedBy { get; set; }
        public string EmployeeName { get; set; }

        public string ExpenseStatus { get; set; }
        public string ApproverNote { get; set; }
        public ICollection<ExpenseFormDetailResponseDto> ExpenseDetails { get; set; }
    }
}
