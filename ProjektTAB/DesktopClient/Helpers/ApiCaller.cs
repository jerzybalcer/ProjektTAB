using System.Net.Http;

namespace DesktopClient.Helpers
{
    public class ApiCaller
    {
        private static HttpClient _httpClient;

        public ApiCaller()
        {
            _httpClient = new HttpClient();
        }

        public static async void Get()
        {

        }

        public static async void Post()
        {

        }
    }
}
