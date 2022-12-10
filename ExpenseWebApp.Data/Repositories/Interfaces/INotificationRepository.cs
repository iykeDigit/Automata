using ExpenseWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWebApp.Data.Repositories.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        IQueryable<Notification> GetAll();
        Task<IEnumerable<Notification>> GetAllByCompanyId(int companyId);
    }
}
