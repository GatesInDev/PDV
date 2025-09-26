namespace PDV.Core.Entities
{
    /// <summary>
    /// Entidade que representa uma categoria de produtos.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Identificador único da categoria.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da categoria.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição da categoria.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// True se a categoria estiver ativa; caso contrário, false.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Timestamp de criação da categoria.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp da última atualização da categoria.
        /// </summary>
        public DateTime UpdatedAt { get; set; }


        /// <summary>
        /// Lista de produtos associados a esta categoria.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>(); // 1 : N
    }
}
