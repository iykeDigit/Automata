using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.ExpenseFromDto
{
    public class DeleteExpenseFormsByStatusDto
    {
        public int CompanyId { get; set; }
        public string Status { get;set; }
    }
}
