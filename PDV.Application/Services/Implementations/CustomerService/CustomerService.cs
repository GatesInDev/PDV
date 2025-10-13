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
    public partial class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do serviço de clientes.
        /// </summary>
        /// <param name="customerRepository">Repositorio do Cliente</param>
        /// <param name="mapper">DI do AutoMapper</param>
        /// <param name="saleRepository">Repositório do Vendas</param>
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
    }
}
