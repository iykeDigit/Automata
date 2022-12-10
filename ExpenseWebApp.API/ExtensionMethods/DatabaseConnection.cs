using ExpenseWebApp.Data.ContextClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ExpenseWebApp.API.Extensions
{
    public static class DatabaseConnection
    {
        public static void AddDbContextAndConfigurations(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContextPool<ExpenseDbContext>(options =>
            {
                string connStr = config["ConnectionStrings:default"];
                options.UseSqlServer(connStr).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}