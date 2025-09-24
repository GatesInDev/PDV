namespace PDV.Application.DTOs.StockTransaction
{
    public class CreateStockTransactionDTO
    {
        /// <summary>
        /// Identificador único do produto associado à transação de estoque.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Definir a quantidade alterada no estoque.
        /// </summary>
        public int QuantityChanged { get; set; }

        /// <summary>
        /// Definir o tipo de transação (e.g., "addition", "removal").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Definir a razão (opcional)  
        /// </summary>
        public string? Reason { get; set; }
    }
}
