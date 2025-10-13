using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas a clientes.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do repositório de clientes.
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo cliente no repositorio.
        /// </summary>
        /// <param name="customer">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        public async Task CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna um cliente pelo seu Id do repositorio.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Entidade com os dados do cliente</returns>
        public async Task<Customer> GetCustomerAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id) ?? null!;
        }

        /// <summary>
        /// Retorna todos os clientes do repositorio.
        /// </summary>
        /// <returns>Uma lista com as entidades dos clientes.</returns>
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Retorna todas as vendas de um cliente específico.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <returns>uma lista com as compras do cliente.</returns>
        public async Task<List<Sale>> GetSaleByCostumerAsync(Guid customerId)
        {
            return await _context.Sales
                .Include(p => p.SaleProducts)
                .Where(s => s.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
