namespace PDV.Application.DTOs.Customer
{
    public class CustomerDTO
    {
        /// <summary>
        /// Saída do identificador único do cliente.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Saída do nome do cliente.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Saída do email do cliente.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Saída da idade do cliente.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Saída do endereço do cliente.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Data de criação do cliente.
        /// </summary>
        public string CreatedAt { get; set; }
    }
}
