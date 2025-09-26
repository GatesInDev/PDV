using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Customer
{
    public class CreateCustomerDTO
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Name { get; set; }

        /// <summary>
        /// Email do cliente.
        /// </summary>
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Idade do cliente.
        /// </summary>
        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(0, 120, ErrorMessage = "A idade deve estar entre 0 e 120 anos.")]
        public int Age { get; set; }

        /// <summary>
        /// Endereço do cliente.
        /// </summary>
        [MaxLength(200, ErrorMessage = "O endereço não pode exceder 200 caracteres.")]
        public string Address { get; set; }

        /// <summary>
        /// Data de criação do cliente.
        /// </summary>
        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CreatedAt { get; set; }
    }
}
