using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseWebApp.Data.ContextClass
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> option) : base(option)
        {
        }

        public DbSet<AdvanceRetirement> AdvanceRetirements { get; set; }
        public DbSet<ExpenseAdvance> ExpenseAdvance { get; set; }
        public DbSet<ExpenseForm> ExpenseForms { get; set; }
        public DbSet<ExpenseFormDetails> ExpenseFormDetails { get; set; }
        public DbSet<PaidFrom> PaidFrom { get; set; }
        public DbSet<ExpenseAccount> ExpenseAccounts { get; set; }
        public DbSet<ExpenseStatus> ExpenseStatus { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CompanyFormData> CompanyFormData { get; set; }


    }

    
}
