using PDV.Application.DTOs.Customer;

namespace PDV.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de clientes.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="createCustomerDto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador do cliente.</returns>
        Task<Guid> CreateCustomerAsync(CreateCustomerDTO createCustomerDto);

        /// <summary>
        /// Retorna um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Objeto com os dados do cliente.</returns>
        Task<CustomerDTO> GetCustomerAsync(Guid id);

        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        /// <returns>Uma lista com os dados dos clientes.</returns>
        Task<List<CustomerDTO>> GetAllCustomersAsync();

        /// <summary>
        /// Retorna todas as vendas de um cliente específico.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Lista com as compras do cliente.</returns>
        Task<List<CustomersAndSalesDTO>> GetSalesByCostumer(Guid id);
    }
}
