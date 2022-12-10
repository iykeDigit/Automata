using AutoMapper;
using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Dtos;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.Pagination;
using ExpenseWebApp.Utilities.ResourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Response<PaginatorHelper<IEnumerable<string>>>> GetAllExpenseCategories(PagingDto paging)
        {
            var categories = _unitOfWork.ExpenseCategory.GetAllExpenseCategory().Select(x => x.ExpenseCategoryName);
            var paginatedResult = await categories.PaginateAsync<string, string>(paging.PageSize, paging.PageNumber, _mapper);
            return Response<PaginatorHelper<IEnumerable<string>>>.Success(ResourceFile.Success, paginatedResult);
        }


    }
}
