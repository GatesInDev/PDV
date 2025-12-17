using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Interfaces;
using System.Windows.Input;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels.Implementations;

public class LoginViewModel : Notifier, ILoginViewModel
{
    private string? _username;
    private string? _errorMessage;
    private bool _isBusy;
    private readonly IApiClient _apiClient;
    private readonly IAuthenticationService _authService;

    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
            ((RelayCommand<object>)LoginCommand).NotifyCanExecuteChanged();
        }
    }

    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
            ((RelayCommand<object>)LoginCommand).NotifyCanExecuteChanged();
        }
    }

    public ICommand LoginCommand { get; }
    public event Action<Tuple<bool, string>>? LoginData;

    public LoginViewModel(IApiClient apiClient, IAuthenticationService authService)
    {
        _apiClient = apiClient;
        _authService = authService;
        LoginCommand = new RelayCommand<object>(Login, CanLogin);
        IsBusy = false;
    }

    private bool CanLogin(object? _)
    {
        return !IsBusy && !string.IsNullOrWhiteSpace(Username);
    }

    private async void Login(object? parameter)
    {
        IsBusy = true;
        ErrorMessage = null;

        if (!(parameter is Wpf.Ui.Controls.PasswordBox passwordBox))
        {
            return;
        }

        try
        {
            var loginResult = await _apiClient.AutenticarAsync(_username, passwordBox.Password);

            if (loginResult != null && !string.IsNullOrEmpty(loginResult.Token) && !string.IsNullOrEmpty(loginResult.Role))
            {
                _authService.SetCurrentUsername(_username!);
                _authService.SetToken(loginResult.Token);

                LoginData?.Invoke(Tuple.Create(true, loginResult.Role));
            }
            else
            {
                ErrorMessage = "Usuário ou senha inválidos.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
}