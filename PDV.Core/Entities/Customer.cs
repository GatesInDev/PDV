namespace PDV.Core.Entities
{
    /// <summary>
    /// Entidade que representa um cliente.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Identificador único do cliente.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email do cliente.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Idade do cliente.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Endereço do cliente.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Data de criação do cliente.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Navegação para a venda associada ao cliente.
        /// </summary>
        public List<Sale> Sales { get; set; } = new List<Sale>();

        public bool IsActive { get; set; }
    }
}
