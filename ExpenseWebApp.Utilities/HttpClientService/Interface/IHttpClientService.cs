using System.Threading.Tasks;

namespace ExpenseWebApp.Utilities.HttpClientService.Interface
{
    public interface IHttpClientService
    {
        Task<TRes> PostRequestAsync<TReq, TRes>(string requestUrl, TReq requestModel, string baseUrl = null, string token = null)
            where TRes : class
            where TReq : class;
        Task<TRes> GetRequestAsync<TRes>(string requestUrl, string baseUrl = null, string token = null)
            where TRes: class;
    }
}
