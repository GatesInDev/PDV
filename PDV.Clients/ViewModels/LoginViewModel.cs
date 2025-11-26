using PDV.Clients.Services.Interfaces;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Authentication;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels;

public class LoginViewModel : Notifier
{
    private string? _username;
    private string? _errorMessage;
    private bool _isBusy;

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
    public event Action<Tuple<bool,string>> LoginData; 
    private readonly IApiClient _apiClient;

    public LoginViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
        LoginCommand = new RelayCommand<object>(Login, CanLogin);
        IsBusy = false;
    }

    private bool CanLogin(object? _)
    {
        return !IsBusy
            && !string.IsNullOrWhiteSpace(Username);
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
                LoginData?.Invoke(Tuple.Create(true,loginResult.Role));
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