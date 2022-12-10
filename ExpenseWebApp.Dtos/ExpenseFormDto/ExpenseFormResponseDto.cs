using ExpenseWebApp.Dtos.ExpenseFormDetailsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.ExpenseFormDto
{
    public class ExpenseFormResponseDto
    {
        public string ExpenseFormId { get; set; }
        public string ExpenseFormNo { get; set; }
        public string Description { get; set; }
        public decimal ReimburseableAmount { get => ExpenseDetails.Where(x => x.PaidByCompany == false).Sum(x => x.ExpenseAmount); }
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
