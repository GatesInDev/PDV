using System.Text.Json.Serialization;

namespace PDV.Core.Entities
{
    /// <summary>
    /// Entidade que representa uma transação de estoque.
    /// </summary>
    public class StockTransaction
    {
        /// <summary>
        /// Identificador único da transação de estoque.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Quantidade alterada na transação (pode ser positiva ou negativa).
        /// </summary>
        public int QuantityChanged { get; set; }

        /// <summary>
        /// Tipo de transação (ex: "Entrada", "Saída", "Ajuste").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Timestamp da ultima atualização da transação.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Razão ou descrição da transação.
        /// </summary>
        public string? Reason { get; set; }


        /// <summary>
        /// Identificador do produto ao qual esta transação de estoque pertence.
        /// </summary>
        public Guid ProductId { get; set; } // FK

        /// <summary>
        /// Navegação para a entidade Produto.
        /// </summary>
        [JsonIgnore]
        public Product Product { get; set; } // Navegation Property
    }
}
