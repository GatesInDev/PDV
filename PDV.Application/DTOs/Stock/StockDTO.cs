namespace PDV.Application.DTOs.Stock
{
    /// <summary>
    /// DTO para representar informações de estoque.
    /// </summary>
    public class StockDTO
    {
        /// <summary>
        /// Identificador único do estoque.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Definir o identificador único do produto associado ao estoque.
        /// </summary>
        public Guid ProductId { get; set; } // FK

        /// <summary>
        /// Definir o nome do produto associado ao estoque.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Definir a quantidade disponível no estoque.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Definir a unidade de medida do produto (Un/Kg/Ml).
        /// </summary>
        public string MetricUnit { get; set; }

        /// <summary>
        /// Definir a data de atualização do registro de estoque.
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
}
