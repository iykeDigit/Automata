using ExpenseWebApp.Utilities.MailSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWebApp.API.ExtensionMethods
{
    public static class ConfigureMailService
    {
        public static void MailService(this IServiceCollection services, IConfiguration Configuration)
        {
            var emailConfiguratioin = new MailSettings
            {
                Mail = Configuration["MailSettings:Mail"],
                DisplayName = Configuration["MailSettings:DisplayName"],
                Password = Configuration["MailSettings:Password"],
                Host = Configuration["MailSettings:Host"],
                Port = Convert.ToInt32(Configuration["MailSettings:Port"])
            };

            services.AddSingleton(emailConfiguratioin);

        }
    }
}
