using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.ExpenseFromDto
{
    public class DeleteExpenseFormDto
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string FormNumber { get; set; }
    }
}
