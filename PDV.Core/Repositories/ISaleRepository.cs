using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface ISaleRepository
    {
        /// <summary>
        /// Adiciona uma nova venda no banco de dados.
        /// </summary>
        /// <param name="sale">Objeto com os dados da venda a serem adicionados.</param>
        /// <returns>Sem retorno.</returns>
        public Task AddAsync(Sale sale);

        /// <summary>
        /// Retorna uma venda pelo seu Id do banco de dados.
        /// </summary>
        /// <param name="id">Identificador da venda.</param>
        /// <returns>Entidade com os dados da venda.</returns>
        public Task<Sale> GetByIdAsync(Guid id);

        public Task<List<Sale>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
