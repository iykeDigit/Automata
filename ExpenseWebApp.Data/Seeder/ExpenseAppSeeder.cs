using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ExpenseWebApp.Data.Seeder
{
    public static class ExpenseAppSeeder
    {
        public static async Task PrepopulateDb(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                await SeedData(serviceScope.ServiceProvider.GetService<ExpenseDbContext>());
            }
        }

        private static async Task SeedData(ExpenseDbContext dbContext)
        {
            var baseDir = Directory.GetCurrentDirectory();

            await dbContext.Database.EnsureCreatedAsync();

            if (!dbContext.ExpenseStatus.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/ExpenseStatus.json"));

                var expenseStatus = JsonConvert.DeserializeObject<List<ExpenseStatus>>(path);
                await dbContext.ExpenseStatus.AddRangeAsync(expenseStatus);
            }

            if (!dbContext.ExpenseForms.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/ExpenseForm.json"));

                var expenseForms = JsonConvert.DeserializeObject<List<ExpenseForm>>(path);
                await dbContext.ExpenseForms.AddRangeAsync(expenseForms);
            }

            if (!dbContext.ExpenseCategories.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/ExpenseCategory.json"));

                var expenseCategories = JsonConvert.DeserializeObject<List<ExpenseCategory>>(path);
                await dbContext.ExpenseCategories.AddRangeAsync(expenseCategories);
            }

            if (!dbContext.ExpenseFormDetails.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/ExpenseFormDetails.json"));

                var expenseFormDetails = JsonConvert.DeserializeObject<List<ExpenseFormDetails>>(path);
                await dbContext.ExpenseFormDetails.AddRangeAsync(expenseFormDetails);
            }

            if (!dbContext.PaidFrom.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/PaidFrom.json"));

                var paidFrom = JsonConvert.DeserializeObject<List<PaidFrom>>(path);
                await dbContext.PaidFrom.AddRangeAsync(paidFrom);
            }

            if (!dbContext.ExpenseAdvance.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/AdvanceForm.json"));

                var advanceForms = JsonConvert.DeserializeObject<List<ExpenseAdvance>>(path);
                await dbContext.ExpenseAdvance.AddRangeAsync(advanceForms);
            }

            if (!dbContext.ExpenseAccounts.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/ExpenseAccounts.json"));

                var expenseAccounts = JsonConvert.DeserializeObject<List<ExpenseAccount>>(path);
                await dbContext.ExpenseAccounts.AddRangeAsync(expenseAccounts);
            }

            if (!dbContext.CompanyFormData.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/FormCount.json"));

                var formCount = JsonConvert.DeserializeObject<List<CompanyFormData>>(path);
                await dbContext.CompanyFormData.AddRangeAsync(formCount);
            }


            await dbContext.SaveChangesAsync();
        }

        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}
