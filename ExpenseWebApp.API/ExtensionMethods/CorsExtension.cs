using Microsoft.Extensions.DependencyInjection;

namespace ExpenseWebApp.API.ExtensionMethods
{
    public static class CorsExtension
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });

            });
        }
    }
}
