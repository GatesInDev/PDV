namespace PDV.Application.DTOs.StockTransaction
{
    /// <summary>
    /// DTO para representar transações de estoque.
    /// </summary>
    public class StockTransactionDTO
    {
        /// <summary>
        /// Identificador único da transação de estoque.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Definir o identificador único do produto associado à transação de estoque.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Definir a quantidade alterada do produto no estoque.
        /// </summary>
        public int QuantityChanged { get; set; }

        /// <summary>
        /// Definir o tipo de transação (Entrada ou Saída).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Definir a data e hora da transação de estoque.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Definir o motivo da alteração no estoque.
        /// </summary>
        public string? Reason { get; set; }
    }
}
