using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.Repositories.Implementation;
using ExpenseWebApp.Data.Repositories.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;

namespace ExpenseWebApp.Data.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExpenseDbContext _context;
        private IAdvanceRetirementRepository _advanceRetirement;
        private IExpenseAdvanceRepository _expenseAdvance;
        private IExpenseFormRepository _expenseForm;
        private IExpenseFormDetailsRepository _expenseFormDetails;
        private IPaidFormRepository _paidForm;
        private IExpenseStatusRepository _expenseStatus;
        private IExpenseAccountRepository _expenseAccount;
        private INotificationRepository _notifications;
        private IExpenseCategoryRepository _expenseCategory;
        private ICompanyFormDataRepository _companyFormDataRepository;


        public UnitOfWork(ExpenseDbContext context)
        {
            _context = context;
        }



        public IAdvanceRetirementRepository AdvanceRetirement =>
            _advanceRetirement ??= new AdvanceRetirementRepository(_context);


        public IExpenseAdvanceRepository ExpenseAdvance => _expenseAdvance ??= new ExpenseAdvanceRepository(_context);

        public IExpenseFormRepository ExpenseForm => _expenseForm ??=
            new ExpenseFormRepository(_context);

        public IExpenseFormDetailsRepository ExpenseFormDetails => _expenseFormDetails ??=
            new ExpenseFormDetailsRepository(_context);

        public IExpenseStatusRepository ExpenseStatus => _expenseStatus ??=
            new ExpenseStatusRepository(_context);

        public IPaidFormRepository PaidForm => _paidForm ??= new PaidFormRepository(_context);

        public IExpenseAccountRepository ExpenseAccount => _expenseAccount ??= new ExpenseAccountRepository(_context);
        public INotificationRepository Notifications => _notifications ??= new NotificationRepository(_context);
        public IExpenseCategoryRepository ExpenseCategory => _expenseCategory ??= new ExpenseCategoryRepository(_context);
        public ICompanyFormDataRepository CompanyFormDataRepository => _companyFormDataRepository ??= new CompanyFormDataRepository(_context);
        

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
