namespace PDV.Core.Entities
{
    /// <summary>
    /// Representa um usuário do sistema utilizado para autenticação JWT.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome de usuário utilizado para login.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Senha do usuário (armazenada de forma segura).
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Papel ou perfil de acesso do usuário (por exemplo: Admin, Operador).
        /// </summary>
        public string Role { get; set; }
    }
}
