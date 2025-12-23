using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.User
{
    public class UpdateUserDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        public string? Password { get; set; }

        [Required]
        public string Role { get; set; } = "Operador";
    }
}
