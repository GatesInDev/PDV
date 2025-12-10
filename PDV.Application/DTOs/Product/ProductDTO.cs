namespace PDV.Application.DTOs.Product
{
    /// <summary>
    /// DTO para exibição básica de um produto na lista.
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Saída do identificador único do produto.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Saída do Stock Keep Unit do produto.
        /// </summary>
        public required string Sku { get; set; }

        /// <summary>
        /// Saída do nome do produto.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Saída do preço do produto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Saída do nome da categoria do produto.
        /// </summary>
        public required string CategoryName { get; set; }

        /// <summary>
        /// Saída da unidade de medida do produto (Un/Kg/Ml).
        /// </summary>
        public required string MetricUnit { get; set; }

        /// <summary>
        /// Saída que identifica se o produto está ativo.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
