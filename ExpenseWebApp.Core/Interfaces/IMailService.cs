using ExpenseWebApp.Utilities.MailSettings;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(MailRequest mailRequest);
    }
}