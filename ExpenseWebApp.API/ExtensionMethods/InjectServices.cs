using ExpenseWebApp.Core.Implementation;
using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Data.UnitOfWork.Implementations;
using ExpenseWebApp.Utilities.HttpClientService.Implementation;
using ExpenseWebApp.Utilities.HttpClientService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseWebApp.API.ExtensionMethods
{
    public static class InjectServices
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpClientService, HttpClientService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUpdateFormStatus, UpdateFormStatus>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IExpenseFormService, ExpenseFormService>();
            services.AddScoped<IExpenseAdvance, ExpenseAdvanceService>();
            services.AddScoped<IExpenseAccountService, ExpenseAccountService>();
            services.AddScoped<IExpenseFormDetails, ExpenseFormDetailsService>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
            services.AddScoped<IFormNumberGeneratorService, FormNumberGeneratorService>();
            services.AddScoped<IFormNumberService, FormNumberService>();

            services.AddTransient<IMailService, MailService>();
            services.AddTransient<INotificationService, NotificationService>();
            
            
            //Image service injected
            services.AddScoped<IAttachmentService, AttachmentService>();
        }
    }
}
