using ExpenseWebApp.Dtos.ExpenseAdvanceDtos;
using ExpenseWebApp.Dtos.ExpenseFormDetailsDtos;
using ExpenseWebApp.Dtos.ExpenseFormDto;
using ExpenseWebApp.Dtos.ExpenseFromDto;
using ExpenseWebApp.Utilities.AppFluentValidation;
using ExpenseWebApp.Utilities.AppFluentValidation.ExpenseAdvanceValidations;
using ExpenseWebApp.Utilities.AppFluentValidation.ExpenseFormValidations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseWebApp.API.ExtensionMethods
{
    public static class FluentValidations 
    {
        public static void InjectFluentValidations(this IServiceCollection service)
        {
            service.AddTransient<IValidator<DeleteExpenseDetailsFormDto>, DeleteExpenseDetailsFormDtoValidator>();
            service.AddTransient<IValidator<DeleteExpenseFormDto>, DeleteExpenseFormDtoValidator>();
            service.AddTransient<IValidator<DeleteExpenseFormsByStatusDto>, DeleteExpenseFormsByStatusDtoValidator>();
            service.AddTransient<IValidator<ExpenseFormDetailDto>, EditExpenseFormDtoValidator>();
            service.AddTransient<IValidator<CreateExpenseAdvanceDto>, CreateExpenseAdvanceDtoValidation>();
            service.AddTransient<IValidator<UpdateExpenseAdvanceDto>, UpdateExpenseAdvanceFormDtoValidation>();
            service.AddTransient<IValidator<ExpenseFormCreateRequestDto>, ExpenseFormCreateRequestDtoValidator>();
        }
    }
}