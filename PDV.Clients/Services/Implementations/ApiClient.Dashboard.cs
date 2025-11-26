using PDV.Clients.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PDV.Application.DTOs;
using PDV.Application.DTOs.Customer;
using PDV.Application.DTOs.Metrics;

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
                string endpoint = $"api/Sales?startDate={DateTime.Today:yyyy-MM-dd}&endDate={DateTime.Today:yyyy-MM-dd}";
                return await _httpClient.GetFromJsonAsync<IEnumerable<TransactionModel>>(endpoint);
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