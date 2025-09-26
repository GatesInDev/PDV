﻿using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Cria um novo cliente no repositorio.
        /// </summary>
        /// <param name="customer">Objeto com os dados do cliente.</param>
        /// <returns>Sem retorno.</returns>
        Task CreateCustomerAsync(Customer customer);

        /// <summary>
        /// Retorna um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Retorna uma entidade com os dados do cliente.</returns>
        Task<Customer> GetCustomerAsync(Guid id);

        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        /// <returns>Retorna uma lista com entidade de todos os clientes.</returns>
        Task<List<Customer>> GetAllCustomersAsync();
    }
}
