namespace PDV.Clients.ViewModels.Implementations.User;

/// <summary>
/// ViewModel para exibição de usuário na lista.
/// </summary>
public class UserListItemViewModel
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}