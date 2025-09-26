using AutoMapper;
using PDV.Application.DTOs.Customer;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para gerenciar operações relacionadas a clientes.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do serviço de clientes.
        /// </summary>
        /// <param name="customerRepository">Repositorio do Cliente</param>
        /// <param name="mapper">DI do AutoMapper</param>
        /// <param name="saleRepository">Repositório do Vendas</param>
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, ISaleRepository saleRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="createCustomerDto">Objeto com os dados que serão criados.</param>
        /// <returns>Identificador do Cliente.</returns>
        public async Task<Guid> CreateCustomerAsync(CreateCustomerDTO createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);

            customer.Id = Guid.NewGuid();
            customer.CreatedAt = DateTime.UtcNow;
            

            await _customerRepository.CreateCustomerAsync(customer);

            return customer.Id;
        }

        /// <summary>
        /// Retorna um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do Cliente.</param>
        /// <returns>Objeto com os dados do cliente.</returns>
        public async Task<CustomerDTO> GetCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetCustomerAsync(id);
            return _mapper.Map<CustomerDTO>(customer);
        }

        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        /// <returns>Uma lista com todos os clientes.</returns>
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        /// <summary>
        /// Retorna todas as vendas de um cliente específico.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Uma lisat com todas as vendas do cliente.</returns>
        public async Task<List<CustomersAndSalesDTO>> GetSalesByCostumer(Guid id)
        {
            var sales = await _saleRepository.GetSaleByCostumerAsync(id);
            return _mapper.Map<List<CustomersAndSalesDTO>>(sales);
        }
    }
}
