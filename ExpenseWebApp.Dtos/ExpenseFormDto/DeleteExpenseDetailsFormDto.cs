using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.ExpenseFromDto
{
    public class DeleteExpenseDetailsFormDto
    {
       public int companyId { get; set; }
       public int userId { get; set; }
       public string expenseFormNumber { get; set; }
       public string expenseFormDetailsId { get;set; }
    }
}
