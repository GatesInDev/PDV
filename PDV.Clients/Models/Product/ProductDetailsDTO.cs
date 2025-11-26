namespace PDV.Application.DTOs.Product
{
    /// <summary>
    /// DTO para exibição dos detalhes completos de um produto.
    /// </summary>
    public class ProductDetailsDTO
    {
        /// <summary>
        /// Saída do identificador único do produto.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Saída do Stock Keep Unit do produto.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Saída do nome do produto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Saída do preço do produto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Saida que identifica se o produto está ativo.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Saída da unidade de medida do produto(Un/Kg/Ml).
        /// </summary>
        public string MetricUnit { get; set; }

        /// <summary>
        /// Saída da data de criação do produto.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Saída da data da última atualização do produto.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Saída do nome da categoria do produto recebido por Join.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
