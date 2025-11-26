using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PDV.Clients.Services.Interfaces;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        private readonly string _baseUrl = "https://localhost:44360/";

        private string? _token = null;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
