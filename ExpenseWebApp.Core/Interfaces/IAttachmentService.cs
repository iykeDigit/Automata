using ExpenseWebApp.Dtos;
using ExpenseWebApp.Utilities;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IAttachmentService
    {
        Task<string> SaveFilePath(FormDto file);
    }
}
