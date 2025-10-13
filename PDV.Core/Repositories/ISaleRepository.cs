using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    /// <summary>
    /// Interface para o repositório de vendas.
    /// </summary>
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

        /// <summary>
        /// Retorna todas as vendas em um período do repositório.
        /// </summary>
        /// <param name="startDate">Data de inicio.</param>
        /// <param name="endDate">Data de fim.</param>
        /// <returns>Uma lista com as entidades filtradas.</returns>
        public Task<List<Sale>> GetByPeriodAsync(DateTime startDate, DateTime endDate);

    }
}
