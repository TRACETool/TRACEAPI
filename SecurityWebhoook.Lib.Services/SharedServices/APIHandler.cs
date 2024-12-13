using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace SecurityWebhoook.Lib.Services.SharedServices
{
    public class APIHandler : IAPIHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public APIHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory; 
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TReturn> GetAsync<TReturn>(string url, string baseUrl, Dictionary<string,string> headers = null)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            SetClient(client, baseUrl);
            SetHeaders(headers,client);
            var response = await client.GetAsync(url); 
            return await GetResponseContentAsync<TReturn>(response, baseUrl, url);
        }

        public async Task<TReturn> PostAsync<TReturn>(string json, string url, string baseUrl, Dictionary<string,string> headers = null)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            SetClient(client, baseUrl);
            SetHeaders(headers,client);
            var response = await client.PostAsJsonAsync(url, json);
            return await GetResponseContentAsync<TReturn>(response, baseUrl, url);
        }

        public async Task<TReturn> PostAsync<TReturn, TParam>(TParam json, string url, string baseUrl, Dictionary<string, string> headers = null)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            SetClient(client, baseUrl);
            SetHeaders(headers, client);
            var jsonBody = JsonConvert.SerializeObject(json);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            return await GetResponseContentAsync<TReturn>(response, baseUrl, url);
        }

        private void SetClient(HttpClient client, string baseUrl) 
        {
            client.BaseAddress = new Uri(baseUrl);
        
        }

        private void SetHeaders(Dictionary<string, string> headers, HttpClient client) 
        {
            if (headers is not null) 
            { 
                foreach(var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            
            }
        }

        private async Task<TReturn> GetResponseContentAsync<TReturn>(HttpResponseMessage response, string baseUrl, string url)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    return JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync());
                }
                return default(TReturn);
            }
            string value = await response.Content.ReadAsStringAsync();
            throw new Exception($"API call failed: {baseUrl}{url} with error - {value}");
        }
    }
}
