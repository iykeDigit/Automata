using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.ExpenseAdvanceDtos
{
    public class CreateExpenseAdvanceDto
    {
        public string AdvanceDescription { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string AdvanceNote { get; set; }
        public DateTime AdvanceDate { get; set; }
        public string Employee { get; set; }
        [Required]
        public int UserId { get; set; }
        public string AdvancePurpose { get; set; }
        [Required]
        public string CACNumber { get; set; }
        [Required]
        public string Token { get; set; }

    }
}
