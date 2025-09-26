namespace PDV.Application.DTOs.Sales
{
    /// <summary>
    /// DTO para criação de produtos em uma venda.
    /// </summary>
    public class CreateSaleProductDTO
    {
        /// <summary>
        /// Entrada do identificador único do produto.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Entrada da quantidade do produto.
        /// </summary>
        public int Quantity { get; set; }
    }
}
