namespace PDV.Application.DTOs.Metrics
{
    public class GetBelowStockDTO
    {
        /// <summary>
        /// Identificador único do estoque.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Quantidade disponível no estoque.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unidade de medida do produto (Un/Kg/Ml).
        /// </summary>
        public string MetricUnit { get; set; }

        /// <summary>
        /// Timestamp da última atualização do estoque.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Identificador do produto ao qual este estoque pertence.
        /// </summary>
        public Guid ProductId { get; set; } // FK

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string ProductName { get; set; }
    }
}
