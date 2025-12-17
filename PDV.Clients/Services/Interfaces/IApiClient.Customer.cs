using PDV.Application.DTOs.Customer;
using PDV.Application.DTOs.Sales;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        Task CreateCustomerAsync(CreateCustomerDTO customer);

        Task UpdateCustomerAsync(Guid id, UpdateCustomerDTO customer);

        Task DeleteCustomerAsync(Guid id);

        Task<CustomerDTO> GetCustomerByIdAsync(Guid id);

        Task<List<CustomerDTO>> GetAllCustomersAsync();

        //Task<List<CustomersAndSalesDTO>> GetSaleByCostumerAsync(Guid customerId);
    }
}
