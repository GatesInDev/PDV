using PDV.Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        public async Task CreateCustomerAsync(CreateCustomerDTO customer)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("api/Customer", customer);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the customer.", ex);
            }
        }

        public async Task UpdateCustomerAsync(Guid id, UpdateCustomerDTO customer)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"api/Customer/{id}", customer);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the customer.", ex);
            }
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/Customer/{id}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the customer.", ex);
            }
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Customer/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CustomerDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the customer.", ex);
            }
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Customer");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<CustomerDTO>>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving customers.", ex);
            }
        }
    }
}
