using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Services
{
    public class FileStorageService
    {
        private readonly HttpClient _httpClient;


        public FileStorageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<int> CheckConnection()
        {
            var url = Environment.GetEnvironmentVariable("FILESTORAGESERVICE_URL");
            var route = "/test/test";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url+route);

            var httpResponse = await _httpClient.SendAsync(httpRequestMessage);

            if (httpResponse.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
