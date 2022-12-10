using ExpenseWebApp.Dtos;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IExpenseCategoryService
    {
        Task<Response<PaginatorHelper<IEnumerable<string>>>> GetAllExpenseCategories(PagingDto paging);
    }
}
