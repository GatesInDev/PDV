namespace PDV.Application.DTOs.Stock
{
    public class CreateStockDTO
    {
        /// <summary>
        /// Define o ID do produto ao qual este estoque pertence.
        /// </summary>
        public Guid ProductId { get; set; } // FK

        /// <summary>
        /// Define a quantidade inicial de itens em estoque para o produto.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Define a unidade de medida do estoque (Un/Kg/Ml).
        /// </summary>
        public string MetricUnit { get; set; }
    }
}
