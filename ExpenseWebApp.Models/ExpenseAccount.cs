using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Models
{
    public class ExpenseAccount
    {
        [Key]
        public string ExpenseAccountId { get; set; } = Guid.NewGuid().ToString();
        public string ExpenseAccountName { get; set; }
        public string ExpenseAccountNumber { get; set; }
        public int CompanyId { get; set; }

    }
}
