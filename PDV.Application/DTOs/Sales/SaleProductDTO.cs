namespace PDV.Application.DTOs.Sales
{
    /// <summary>
    /// DTO de intermédiario para representar produtos em uma venda.
    /// </summary>
    public class SaleProductDTO
    {
        /// <summary>
        /// Identificador único do produto.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Preço do produto no momento da venda.
        /// </summary>
        public decimal PriceAtSaleTime { get; set; }

        /// <summary>
        /// Quantidade do produto na venda.
        /// </summary>
        public int Quantity { get; set; }
    }
}
