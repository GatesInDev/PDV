namespace PDV.Core.Entities
{
    public class Stock
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
        /// Navegação para a entidade Produto.
        /// </summary>
        public Product Product { get; set; } // Navigation Property
    }
}
