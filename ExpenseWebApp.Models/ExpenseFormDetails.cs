using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Models
{
    public class ExpenseFormDetails
    {
        [Key]
        public string ExpenseFormDetailsId { get; set; } = Guid.NewGuid().ToString();
        public DateTime ExpenseDate { get; set; }
        public decimal ExpenseAmount { get; set; }
        public bool PaidByCompany { get; set; }
        public string ExpenseNote { get; set; }
        public string Attachments { get; set; }
        public string ExpenseFormId { get; set; }
        public string ExpenseCategoryId { get; set; }

        //Navigation Properties
        public ExpenseForm ExpenseForm { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}