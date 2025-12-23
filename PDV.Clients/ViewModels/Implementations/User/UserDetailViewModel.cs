namespace PDV.Clients.ViewModels.Implementations.User;

public class UserDetailViewModel : Notifier
{
    private Guid _id;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _role = "Operador";

    public Guid Id
    {
        get => _id;
        set { _id = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsNewUser)); }
    }

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
    }

    public string Role
    {
        get => _role;
        set { _role = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
    }

    public bool IsNewUser => Id == Guid.Empty;
    public bool IsValid
    {
        get
        {
            bool passOk = !IsNewUser || !string.IsNullOrWhiteSpace(Password);

            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Role) &&
                   passOk;
        }
    }
}