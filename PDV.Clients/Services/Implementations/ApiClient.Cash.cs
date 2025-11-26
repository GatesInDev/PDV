using PDV.Application.DTOs.Cash;
using PDV.Clients.Models.Cash;
using System.Net.Http.Json;
using System.Text.Json;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        public async Task<List<CashSessionDTO>> GetCashSessionsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Cash");

                if (!response.IsSuccessStatusCode)
                    return new List<CashSessionDTO>();

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                    return new List<CashSessionDTO>();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                using var doc = JsonDocument.Parse(content);

                if (doc.RootElement.ValueKind == JsonValueKind.Object)
                {
                    var session = JsonSerializer.Deserialize<CashSessionDTO>(content, options);
                    return session != null ? new List<CashSessionDTO> { session } : new();
                }
                else if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    var list = JsonSerializer.Deserialize<List<CashSessionDTO>>(content, options);
                    return list ?? new();
                }

                return new List<CashSessionDTO>();
            }
            catch
            {
                return new List<CashSessionDTO>();
            }
        }

        public async Task<bool> OpenCashSessionAsync(OpenCashSessionDTO dto)
        {
            try
            {
                using var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync("/api/Cash/open", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CloseCashSessionAsync(CloseCashSessionDTO dto)
        {
            try
            {
                using var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync("/api/Cash/close", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}