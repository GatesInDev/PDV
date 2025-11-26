using PDV.Clients.Models.Auth;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        
        public async Task<LoginResponseModel?> AutenticarAsync(string username, string password)
        {
            string endpoint = "api/Auth/login";

            var loginData = new LoginRequestModel
            {
                Username = username,
                Password = password
            };

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, loginData);

                if (response.IsSuccessStatusCode)
                {
                    var loginResult = await response.Content.ReadFromJsonAsync<LoginResponseModel>();

                    if (loginResult != null && !string.IsNullOrEmpty(loginResult.Token))
                    {
                        _token = loginResult.Token;

                        _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", _token);
                    }

                    return loginResult;
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"Erro de rede: {ex.Message}");
                return null;
            }

            return null;
        }
    }
}
