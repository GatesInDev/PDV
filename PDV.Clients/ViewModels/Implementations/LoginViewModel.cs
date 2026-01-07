using PDV.Clients.Services.Implementations;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Interfaces;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

    private string _storeName = "PDV System";
    public string StoreName
    {
        get => _storeName;
        set { _storeName = value; OnPropertyChanged(); }
    }

    private ImageSource _storeLogo;
    public ImageSource StoreLogo
    {
        get => _storeLogo;
        set { _storeLogo = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }
    public event Action<Tuple<bool, string>>? LoginData;

    public LoginViewModel(IApiClient apiClient, IAuthenticationService authService)
    {
        _apiClient = apiClient;
        _authService = authService;
        LoginCommand = new RelayCommand<object>(Login, CanLogin);
        IsBusy = false;

        LoadStoreInfo();
    }

    private async void LoadStoreInfo()
    {
        try
        {
            var config = await _apiClient.GetConfigAsync();
            if (config != null)
            {
                if (!string.IsNullOrEmpty(config.NomeFantasia))
                    StoreName = config.NomeFantasia;

                if (config.Logo != null && config.Logo.Length > 0)
                {
                    var image = new BitmapImage();
                    using (var mem = new MemoryStream(config.Logo))
                    {
                        mem.Position = 0;
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = mem;
                        image.EndInit();
                    }
                    image.Freeze();
                    StoreLogo = image;
                }
            }
        }
        catch {  }
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