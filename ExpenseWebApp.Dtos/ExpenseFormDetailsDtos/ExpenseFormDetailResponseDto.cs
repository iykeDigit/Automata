using System;

namespace ExpenseWebApp.Dtos.ExpenseFormDetailsDtos
{
    public class ExpenseFormDetailResponseDto
    {
        public string ExpenseFormDetailsId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal ExpenseAmount { get; set; }
        public string Attachments { get; set; }
        public string ExpenseFormId { get; set; }
        public string ExpenseCategoryName { get; set; }
        public bool PaidByCompany { get; set; }
        public string ExpenseNote { get; set; }
    }
}
