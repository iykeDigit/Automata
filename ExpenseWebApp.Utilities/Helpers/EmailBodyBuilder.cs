using System.IO;
using System.Threading.Tasks;

namespace ExpenseWebApp.Utilities.Helpers
{
    public class EmailBodyBuilder
    {
        public static async Task<string> GetEmailBody(string emailTempPath, string token, string email)
        {
            var link = $"";
            var temp = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), emailTempPath));
            var emailBody = temp.Replace("**link**", link);
            return emailBody;
        }
    }
}
