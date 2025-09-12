namespace PDV.Application.DTOs.Category
{
    /// <summary>
    /// DTO para detalhes completos de uma categoria.
    /// </summary>
    public class CategoryDetailsDTO
    {
        /// <summary>
        /// Saída do identificador único da categoria.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Saída do nome da categoria.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Saída da descrição da categoria.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Saída que identifica se a categoria está ativa.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Saída da data de criação da categoria.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Saída da data da última atualização da categoria.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}

