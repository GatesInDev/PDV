using System.Text.Json.Serialization;

namespace PDV.Application.DTOs.Metrics
{
    public class GetBestSellersMetricsDTO
    {
        /// <summary>
        /// Identificador único do produto.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Stock Keeping Unit - Código único para controle de estoque.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Preço do produto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantidade vendida.
        /// </summary>
        public int? SaledQuantity { get; set; }

        /// <summary>
        /// True se o produto estiver ativo; caso contrário, false.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Unidade de medida do produto (ex: kg, unidade, litro).
        /// </summary>
        public string MetricUnit { get; set; }

        /// <summary>
        /// Timestamp de criação do produto.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Atualização da última modificação do produto.
        /// </summary>
        public DateTime UpdatedAt { get; set; }


        /// <summary>
        /// Identificador da categoria à qual o produto pertence.
        /// </summary>
        public int CategoryId { get; set; } // FK

        /// <summary>
        /// Nome da Categoria.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Navegação para a entidade Categoria.
        /// </summary>
        [JsonIgnore]
        public PDV.Core.Entities.Category Category { get; set; } // Navigation Property

    }
}
