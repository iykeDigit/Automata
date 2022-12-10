using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Utilities
{
    public class ClientResponse<T>
    {
        public int CompanyId { get; set; }
        public T UserInfo { get; set; }
        public T Roles { get; set; }

    }
}
