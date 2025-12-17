namespace PDV.Clients.Services.Interfaces;

/// <summary>
/// Interface para gerenciar a autenticação do usuário logado.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Obtém o nome do usuário atualmente logado.
    /// </summary>
    /// <returns>Nome de usuário ou null se não estiver logado.</returns>
    string? GetCurrentUsername();

    /// <summary>
    /// Define o nome do usuário após login.
    /// </summary>
    /// <param name="username">Nome de usuário a ser armazenado.</param>
    void SetCurrentUsername(string username);

    /// <summary>
    /// Define o token JWT após login.
    /// </summary>
    /// <param name="token">Token JWT a ser armazenado.</param>
    void SetToken(string token);

    /// <summary>
    /// Obtém o token JWT armazenado.
    /// </summary>
    /// <returns>Token JWT ou null se não estiver logado.</returns>
    string? GetToken();

    /// <summary>
    /// Limpa as informações de autenticação (logout).
    /// </summary>
    void Clear();
}