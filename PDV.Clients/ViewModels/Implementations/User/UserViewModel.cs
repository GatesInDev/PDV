using PDV.Application.DTOs.User;
using PDV.Clients.Services.Implementations;
using PDV.Clients.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels.Implementations.User;

public class UserViewModel : Notifier
{
    private readonly IApiClient _apiClient;

    private ObservableCollection<UserListItemViewModel> _users = new();
    private UserListItemViewModel? _selectedListItem;
    private UserDetailViewModel? _selectedUser;

    private bool _isBusy;
    private string? _errorMessage;
    private bool _hasDashboardAccess;
    private string _backButtonText = "Voltar";

    #region Properties

    public ObservableCollection<UserListItemViewModel> Users
    {
        get => _users;
        private set { _users = value; OnPropertyChanged(); }
    }

    public UserListItemViewModel? SelectedListItem
    {
        get => _selectedListItem;
        set
        {
            _selectedListItem = value;
            OnPropertyChanged();
            if (value != null)
            {
                LoadDetailFromList(value);
            }
        }
    }

    public UserDetailViewModel? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
            RefreshCommandStates();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
            RefreshCommandStates();
        }
    }

    public string? ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); }
    }

    public bool HasDashboardAccess
    {
        get => _hasDashboardAccess;
        set
        {
            _hasDashboardAccess = value;
            OnPropertyChanged();
            UpdateBackButtonText();
        }
    }

    public string BackButtonText
    {
        get => _backButtonText;
        set
        {
            _backButtonText = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    public ICommand LoadCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand NewUserCommand { get; }
    public ICommand SaveUserCommand { get; }
    public ICommand DeleteUserCommand { get; }
    public ICommand BackCommand { get; }

    public event Action? RequestClose;

    #endregion

    public UserViewModel(IApiClient apiClient, bool hasDashboardAccess = true)
    {
        _apiClient = apiClient;
        _hasDashboardAccess = hasDashboardAccess;

        LoadCommand = new RelayCommand<object>(OnLoad);
        RefreshCommand = new RelayCommand<object>(OnRefresh, _ => !IsBusy);
        NewUserCommand = new RelayCommand<object>(OnNewUser, _ => !IsBusy);
        SaveUserCommand = new RelayCommand<object>(OnSave, CanSave);
        DeleteUserCommand = new RelayCommand<object>(OnDelete, CanDelete);
        BackCommand = new RelayCommand<object>(OnBack);

        UpdateBackButtonText();
        OnLoad(null);
    }

    private void UpdateBackButtonText()
    {
        BackButtonText = _hasDashboardAccess ? "Voltar" : "Fechar";
    }

    #region Command Handlers

    private async void OnLoad(object? _)
    {
        await LoadDataAsync();
    }

    private async void OnRefresh(object? _)
    {
        SelectedUser = null;
        SelectedListItem = null;
        await LoadDataAsync();
    }

    private void OnNewUser(object? _)
    {
        SelectedListItem = null;

        SelectedUser = new UserDetailViewModel
        {
            Id = Guid.Empty,
            Username = string.Empty,
            Password = string.Empty,
            Role = "Operador"
        };
    }

    private void OnBack(object? _)
    {
        RequestClose?.Invoke();
    }

    private void LoadDetailFromList(UserListItemViewModel item)
    {
        SelectedUser = new UserDetailViewModel
        {
            Id = item.Id,
            Username = item.Username,
            Password = string.Empty,
            Role = item.Role
        };
    }


    private async void OnSave(object? _)
    {
        if (SelectedUser == null) return;

        var username = SelectedUser.Username?.Trim() ?? string.Empty;
        var password = SelectedUser.Password?.Trim() ?? string.Empty;
        var role = SelectedUser.Role?.Trim() ?? "Operador";

        ErrorMessage = null; 

        if (string.IsNullOrWhiteSpace(username))
        {
            ErrorMessage = "O Nome de Usuário é obrigatório.";
            return;
        }

        if (username.Length < 3 || username.Length > 20)
        {
            ErrorMessage = "O usuário deve ter entre 3 e 20 caracteres.";
            return;
        }

        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9\._]+$"))
        {
            ErrorMessage = "O usuário deve conter apenas letras, números, ponto (.) ou underline (_). Sem espaços.";
            return;
        }

        if (Regex.IsMatch(username, @"^(admin|root|su|system|administrator)$", RegexOptions.IgnoreCase))
        {
            ErrorMessage = "Este nome de usuário é reservado pelo sistema.";
            return;
        }

        bool isNewUser = SelectedUser.Id == Guid.Empty;
        bool isPasswordProvided = !string.IsNullOrEmpty(password);

        if (isNewUser && !isPasswordProvided)
        {
            ErrorMessage = "A senha é obrigatória para novos usuários.";
            return;
        }

        if (isPasswordProvided)
        {
            if (password.Length < 6)
            {
                ErrorMessage = "A senha é muito fraca. Mínimo de 6 caracteres.";
                return;
            }

            if (password.Contains(" "))
            {
                ErrorMessage = "A senha não pode conter espaços em branco.";
                return;
            }

            if (!Regex.IsMatch(password, @"[a-zA-Z]") || !Regex.IsMatch(password, @"[0-9]"))
            {
                ErrorMessage = "A senha deve conter letras e números.";
                return;
            }
        }


        var allowedRoles = new[] { "Administrador", "Operador", "Estoquista" };

        if (!allowedRoles.Any(r => r.Equals(role, StringComparison.OrdinalIgnoreCase)))
        {
            ErrorMessage = $"A função '{role}' é inválida. Permissões aceitas: {string.Join(", ", allowedRoles)}.";
            return;
        }

        IsBusy = true;

        try
        {
            if (isNewUser)
            {
                var createDto = new CreateUserDTO
                {
                    Username = username, 
                    Password = password,
                    Role = role          
                };

                await _apiClient.CreateUserAsync(createDto);
            }
            else
            {
                var updateDto = new UpdateUserDTO
                {
                    Username = username,
                    Password = password, 
                    Role = role
                };

                await _apiClient.UpdateUserAsync(SelectedUser.Id, updateDto);
            }

            var msgBox = new MessageBox
            {
                Title = "Sucesso",
                Content = "Usuário salvo com sucesso!",
                CloseButtonText = "OK"
            };
            await msgBox.ShowDialogAsync();

            ErrorMessage = null;
            SelectedUser = null;
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            var msgBox = new MessageBox
            {
                Title = "Erro",
                Content = $"Falha ao salvar: {ex.Message}",
                CloseButtonText = "OK"
            };
            await msgBox.ShowDialogAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void OnDelete(object? _)
    {
        if (SelectedUser == null) return;

        ErrorMessage = null;

        IsBusy = true;
        try
        {
            await _apiClient.DeleteUserAsync(SelectedUser.Id);
            ErrorMessage = null;
            SelectedUser = null;
            await LoadDataAsync();
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

    #endregion

    #region Helper Methods

    private async Task LoadDataAsync()
    {
        IsBusy = true;
        ErrorMessage = null;

        try
        {
            var dtos = await _apiClient.GetAllUsersAsync();

            var users = dtos.Select(u => new UserListItemViewModel
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role
            }).ToList();

            Users = new ObservableCollection<UserListItemViewModel>(users);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erro de conexão: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanSave(object? _) => !IsBusy && SelectedUser != null;

    private bool CanDelete(object? _) => !IsBusy && SelectedUser != null && SelectedUser.Id != Guid.Empty;

    private void RefreshCommandStates()
    {
        ((RelayCommand<object>)SaveUserCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)DeleteUserCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)NewUserCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)RefreshCommand).NotifyCanExecuteChanged();
    }

    #endregion
}