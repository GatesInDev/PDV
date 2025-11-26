using System.ComponentModel.DataAnnotations; // Para as anotações de validação

namespace PDV.Application.DTOs.Category
{
    /// <summary>
    /// DTO para criação de uma nova categoria.
    /// </summary>
    public class CreateCategoryDTO
    {
        /// <summary>
        /// Definir o nome da categoria.
        /// </summary>
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome da categoria não pode exceder 100 caracteres.")]
        public string Name { get; set; }

        /// <summary>
        /// Definir a descrição da categoria.
        /// </summary>
        [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
        [MaxLength(255, ErrorMessage = "A descrição da categoria não pode exceder 255 caracteres.")]
        public string Description { get; set; }
    }
}
