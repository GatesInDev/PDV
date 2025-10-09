namespace PDV.Application.DTOs.Product
{
    /// <summary>
    /// DTO para exibição básica de um produto.
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
        public string Name { get; set; }

        /// <summary>
        /// Saída do nome do produto.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
