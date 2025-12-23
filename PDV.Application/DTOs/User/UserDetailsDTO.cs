namespace PDV.Application.DTOs.User
{
    /// <summary>
    /// DTO para exibição dos detalhes completos de um usuário.
    /// </summary>
    public class UserDetailsDTO
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome de usuário utilizado para login.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Papel ou perfil de acesso do usuário.
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}