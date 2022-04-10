using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Helpers
{
    public static class ApiCaller
    {
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
        //private const string BaseAddress = "https://localhost:7062/";
        private const string BaseAddress = "https://tabbackend.azurewebsites.net/";

        public static async Task<HttpResponseMessage> Get(string requestUri)
        {
            return await _httpClient.GetAsync(requestUri);
        }

        public static async Task<HttpResponseMessage> Post(string requestUri, object content)
        {
            string json = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(requestUri, stringContent);
        }
    }
}
