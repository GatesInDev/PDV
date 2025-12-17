using PDV.Clients.Services.Interfaces;

namespace PDV.Clients.Services.Implementations;

/// <summary>
/// Serviço para gerenciar autenticação do usuário na aplicação cliente.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private string? _currentUsername;
    private string? _token;

    /// <summary>
    /// Obtém o nome do usuário atualmente logado.
    /// </summary>
    public string? GetCurrentUsername() => _currentUsername;

    /// <summary>
    /// Define o nome do usuário após login.
    /// </summary>
    public void SetCurrentUsername(string username) => _currentUsername = username;

    /// <summary>
    /// Define o token JWT após login.
    /// </summary>
    public void SetToken(string token) => _token = token;

    /// <summary>
    /// Obtém o token JWT armazenado.
    /// </summary>
    public string? GetToken() => _token;

    /// <summary>
    /// Limpa as informações de autenticação (logout).
    /// </summary>
    public void Clear()
    {
        _currentUsername = null;
        _token = null;
    }
}