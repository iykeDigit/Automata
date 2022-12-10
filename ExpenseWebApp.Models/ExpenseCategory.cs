using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApp.Models
{
    public class ExpenseCategory
    {
        [Key]
        public string ExpenseCategoryId { get; set; } = Guid.NewGuid().ToString();
        public string ExpenseCategoryName { get; set; }
        public ICollection<ExpenseFormDetails> ExpenseFormDetails { get; set; }

    }
}
