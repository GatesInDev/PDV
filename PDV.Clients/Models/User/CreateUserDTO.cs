using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.User
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role é obrigatório")]
        public string Role { get; set; } = "Operador";
    }
}
