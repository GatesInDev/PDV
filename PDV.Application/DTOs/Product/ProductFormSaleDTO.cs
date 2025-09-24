namespace PDV.Application.DTOs.Product
{
    public class ProductFormSaleDTO
    {
        /// <summary>
        /// Parâmetro de entrada do identificador único do produto.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Definir a quantidade do produto.
        /// </summary>
        public int Quantity { get; set; }
    }
}
