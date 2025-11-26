using PDV.Clients.Models.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;
using PDV.Application.DTOs;
using PDV.Application.DTOs.Customer;
using PDV.Application.DTOs.Metrics;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        Task<DataIndicatorsDTO?> GetSummaryAsync();
        Task<IEnumerable<TransactionModel>?> GetRecentTransactionsAsync(int take = 20);
        Task<LifeModel?> GetLife();

        Task<List<CustomerDTO>> GetCustomersByNameAsync(string name);
    }
}