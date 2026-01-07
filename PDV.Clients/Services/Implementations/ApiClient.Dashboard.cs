using PDV.Application.DTOs;
using PDV.Application.DTOs.Customer;
using PDV.Application.DTOs.Metrics;
using PDV.Clients.Models.Dashboard;
using PDV.Clients.Models.Sales;
using System.Net.Http;
using System.Net.Http.Json;
using PDV.Core.Entities;
using PDV.Domain;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        public async Task<DataIndicatorsDTO?> GetSummaryAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DataIndicatorsDTO?>("api/Metrics/dashboard");
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TransactionModel>?> GetRecentTransactionsAsync(int take = 20)
        {
            try
            {
                string endpoint = $"api/Sales?startDate={DateTime.MinValue:yyyy-MM-dd}&endDate={DateTime.Today.AddDays(1):yyyy-MM-dd}";

                var dtoList = await _httpClient.GetFromJsonAsync<IEnumerable<SaleResultDto>>(endpoint);

                if (dtoList == null)
                    return new List<TransactionModel>();

                var transactions = dtoList.Select(dto => new TransactionModel
                {
                    TransactionId = dto.Id,

                    CustomerName = dto.CustomerName,

                    Amount = dto.TotalAmount,

                    Product = dto.Product,

                    OccurredAt = dto.SaleDate
                });

                return transactions
                    .OrderByDescending(t => t.OccurredAt)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<TransactionModel>();
            }
        }
        public async Task<LifeModel?> GetLife()
            {
                try
                {
                    return await _httpClient.GetFromJsonAsync<LifeModel?>("api/Life/life");
                }
                catch (HttpRequestException)
                {
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }

        public async Task<List<CustomerDTO>> GetCustomersByNameAsync(string name)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CustomerDTO?>>($"api/Customer/GetByName/{name}");
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}