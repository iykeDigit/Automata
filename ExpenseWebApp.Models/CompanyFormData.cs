using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Models
{
    [Index(nameof(CACNumber), IsUnique = true)]
    public class CompanyFormData
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string CACNumber { get; set; }
        [Required]
        public int ExpenseFormCount { get; set; }
        [Required]
        public int CashAdvanceFormCount { get; set; }
        [Required]
        public int CashAdvanceRetirementFormCount { get; set; }
        [Required]
        public string TransactionDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    }
}
