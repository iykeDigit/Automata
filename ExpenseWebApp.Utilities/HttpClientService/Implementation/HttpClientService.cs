using ExpenseWebApp.Utilities.HttpClientService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ExpenseWebApp.Utilities.HttpClientService.Implementation
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _url;

        public HttpClientService(IHttpClientFactory clientFactory,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _url = configuration["BaseUrl"];
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TRes> GetRequestAsync<TRes>(string url, string baseUrl = null, string token = null) where TRes : class
        {
            var client = CreateClient(baseUrl, token);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return await GetResponseResultAsync<TRes>(client, request);
        }

        public async Task<TRes> PostRequestAsync<TReq, TRes>(string url, TReq requestModel, string baseUrl = null, string token = null)
            where TRes : class
            where TReq : class
        {
            var client = CreateClient(baseUrl, token);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(requestModel), null, "application/json")
            };
            return await GetResponseResultAsync<TRes>(client, request);
        }

        private async Task<TRes> GetResponseResultAsync<TRes>(HttpClient client, HttpRequestMessage request) where TRes : class
        {
            var response = await client.SendAsync(request);
            var res = new Response<string>();
            var responseString = await response.Content.ReadAsStringAsync();
            res.Data = responseString;

            var result = JsonConvert.DeserializeObject<TRes>(responseString);

            return response.IsSuccessStatusCode ? result : throw new ArgumentException(response.ReasonPhrase);
        }
        private HttpClient CreateClient(string baseUrl, string token = null)
        {
            baseUrl ??= _url;
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(baseUrl);

            // token ??= _httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
            return client;
        }
    }
}
