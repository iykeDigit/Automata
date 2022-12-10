using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseWebApp.API.ExtensionMethods
{
    public static class MVCExtension
    {
        public static void AddMVCConfiguration(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(Validation =>
            {
                Validation.DisableDataAnnotationsValidation = true;
                Validation.RegisterValidatorsFromAssemblyContaining<Startup>();
                Validation.ImplicitlyValidateChildProperties = true;
            });
        }
    }
}
