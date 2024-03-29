﻿using DesktopClient.Authentication;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Helpers
{
    public static class ApiCaller
    {
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
        private const string BaseAddress = "https://localhost:7062/";
        //private const string BaseAddress = "https://tabbackend.azurewebsites.net/";

        public static async Task<HttpResponseMessage> Get(string requestUri)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RetrieveToken());
            return await _httpClient.GetAsync(requestUri);
        }

        public static async Task<HttpResponseMessage> Post(string requestUri, object content)
        {
            string json = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RetrieveToken());
            return await _httpClient.PostAsync(requestUri, stringContent);
        }

        private static string RetrieveToken()
        {
            var token = CurrentAccount.TokensPair.Token;

            if(token is null)
            {
                return "";
            }
            else
            {
                return token;
            }
        }
    }
}
