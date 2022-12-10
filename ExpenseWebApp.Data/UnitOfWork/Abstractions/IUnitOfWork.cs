using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Data.Repositories.Interfaces;

namespace ExpenseWebApp.Data.UnitOfWork.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IAdvanceRetirementRepository AdvanceRetirement { get; }
        IExpenseAdvanceRepository ExpenseAdvance { get; }
        IExpenseFormRepository ExpenseForm { get; }
        IExpenseAccountRepository ExpenseAccount { get; }
        IExpenseFormDetailsRepository ExpenseFormDetails { get; }
        IPaidFormRepository PaidForm { get; }
        IExpenseStatusRepository ExpenseStatus { get; }
        ICompanyFormDataRepository CompanyFormDataRepository { get; }
        INotificationRepository Notifications { get; }
        IExpenseCategoryRepository ExpenseCategory { get; }
        

        Task SaveAsync();
    }
}
