using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Dtos.CompanyDtos
{
    public class GetCompanyResponseDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
