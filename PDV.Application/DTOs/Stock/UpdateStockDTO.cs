namespace PDV.Application.DTOs.Stock
{
    public class UpdateStockDTO
    {
        /// <summary>
        /// Identificador único do produto associado ao estoque.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Definir a quantidade do produto no estoque.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Definir a unidade de medida do produto (Un/Kg/Ml).
        /// </summary>
        public string MetricUnit { get; set; }
    }
}
