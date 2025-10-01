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
            try
            {
                var customer = _mapper.Map<Customer>(createCustomerDto);

                customer.Id = Guid.NewGuid();
                customer.CreatedAt = DateTime.UtcNow;
            
                if (char.IsLower(customer.Name[0]))
                    throw new Exception("O nome do cliente deve começar com letra maiúscula.");

                if (string.IsNullOrWhiteSpace(customer.Name) || customer.Name.Length < 3)
                    throw new Exception("O nome do cliente deve ter pelo menos 3 caracteres.");

                await _customerRepository.CreateCustomerAsync(customer);

                return customer.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do Cliente.</param>
        /// <returns>Objeto com os dados do cliente.</returns>
        public async Task<CustomerDTO> GetCustomerAsync(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerAsync(id);

                if (customer == null)
                    throw new Exception("Cliente não encontrado.");

                var map = _mapper.Map<CustomerDTO>(customer);

                if (map == null)
                    throw new Exception("Erro ao mapear cliente.");

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        /// <returns>Uma lista com todos os clientes.</returns>
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerRepository.GetAllCustomersAsync();

                if (customers == null)
                    throw new Exception("Nenhum cliente encontrado.");

                var map = _mapper.Map<List<CustomerDTO>>(customers);

                if (map == null)
                    throw new Exception("Erro ao mapear clientes.");

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter clientes: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna todas as vendas de um cliente específico.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Uma lisat com todas as vendas do cliente.</returns>
        public async Task<List<CustomersAndSalesDTO>> GetSalesByCostumer(Guid id)
        {
            try
            {
                var sales = await _saleRepository.GetSaleByCostumerAsync(id);

                if (sales == null || !sales.Any())
                    throw new Exception("Nenhuma venda encontrada para este cliente.");


                var map = _mapper.Map<List<CustomersAndSalesDTO>>(sales);

                if (map == null)
                    throw new Exception("Erro ao mapear vendas do cliente.");

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter histórico de compras: {ex.Message}");
            }
        }
    }
}
