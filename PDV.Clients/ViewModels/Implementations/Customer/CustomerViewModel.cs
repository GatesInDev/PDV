using PDV.Application.DTOs.Customer;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf.Ui.Input;
using Wpf.Ui.Controls;
using System.Linq;

namespace PDV.Clients.ViewModels.Implementations.Customer;

public class CustomerViewModel : Notifier, ICustomerViewModel
{
    private readonly IApiClient _apiClient;

    private ObservableCollection<CustomerListItemViewModel> _customers = new();
    private CustomerListItemViewModel? _selectedListItem;
    private CustomerDetailViewModel? _selectedCustomer;

    private bool _isBusy;
    private string? _errorMessage;

    #region Properties

    public ObservableCollection<CustomerListItemViewModel> Customers
    {
        get => _customers;
        private set { _customers = value; OnPropertyChanged(); }
    }

    public CustomerListItemViewModel? SelectedListItem
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

    public CustomerDetailViewModel? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            _selectedCustomer = value;
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

    #endregion

    #region Commands

    public ICommand LoadCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand NewCustomerCommand { get; }
    public ICommand SaveCustomerCommand { get; }
    public ICommand DeleteCustomerCommand { get; }
    public ICommand BackCommand { get; }

    public event Action? RequestClose;

    #endregion

    public CustomerViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;

        LoadCommand = new RelayCommand<object>(OnLoad);
        RefreshCommand = new RelayCommand<object>(OnRefresh, _ => !IsBusy);
        NewCustomerCommand = new RelayCommand<object>(OnNewCustomer, _ => !IsBusy);
        SaveCustomerCommand = new RelayCommand<object>(OnSave, CanSave);
        DeleteCustomerCommand = new RelayCommand<object>(OnDelete, CanDelete);
        BackCommand = new RelayCommand<object>(OnBack);

        OnLoad(null);
    }

    #region Command Handlers

    private async void OnLoad(object? _)
    {
        await LoadDataAsync();
    }

    private async void OnRefresh(object? _)
    {
        SelectedCustomer = null;
        SelectedListItem = null;
        await LoadDataAsync();
    }

    private void OnNewCustomer(object? _)
    {
        SelectedListItem = null;

        SelectedCustomer = new CustomerDetailViewModel
        {
            Id = Guid.Empty,
            Name = string.Empty,
            Email = string.Empty,
            Age = 0,
            Address = string.Empty
        };
    }

    private void OnBack(object? _)
    {
        RequestClose?.Invoke();
    }

    private void LoadDetailFromList(CustomerListItemViewModel item)
    {
        SelectedCustomer = new CustomerDetailViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Email = item.Email,
            Age = item.Age,
            Address = item.Address
        };
    }

    private async void OnSave(object? _)
    {
        if (SelectedCustomer == null) return;

        if (!SelectedCustomer.IsValid)
        {
            ErrorMessage = "Preencha todos os campos obrigatórios (*).";
            return;
        }

        IsBusy = true;
        ErrorMessage = null;

        try
        {
            var createDto = new CreateCustomerDTO
            {
                Name = SelectedCustomer.Name,
                Email = SelectedCustomer.Email,
                Age = SelectedCustomer.Age,
                Address = SelectedCustomer.Address
            };

            var updateDto = new UpdateCustomerDTO
            {
                Name = SelectedCustomer.Name,
                Email = SelectedCustomer.Email,
                Age = SelectedCustomer.Age,
                Address = SelectedCustomer.Address
            };

            if (SelectedCustomer.IsNewCustomer)
            {
                await _apiClient.CreateCustomerAsync(createDto);
            }
            else
            {
                await _apiClient.UpdateCustomerAsync(SelectedCustomer.Id, updateDto);
            }

            var msgBox = new MessageBox { Title = "Sucesso", Content = "Salvo com sucesso!", CloseButtonText = "OK" };
            await msgBox.ShowDialogAsync();

            SelectedCustomer = null;
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            var msgBox = new MessageBox { Title = "Erro", Content = ex.Message, CloseButtonText = "OK" };
            await msgBox.ShowDialogAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void OnDelete(object? _)
    {
        if (SelectedCustomer == null) return;

        var confirmBox = new MessageBox
        {
            Title = "Confirmação",
            Content = $"Excluir '{SelectedCustomer.Name}'?",
            PrimaryButtonText = "Sim",
            CloseButtonText = "Não"
        };

        if (await confirmBox.ShowDialogAsync() == MessageBoxResult.Primary)
        {
            IsBusy = true;
            try
            {
                await _apiClient.DeleteCustomerAsync(SelectedCustomer.Id);
                SelectedCustomer = null;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                var msgBox = new MessageBox { Title = "Erro", Content = ex.Message, CloseButtonText = "OK" };
                await msgBox.ShowDialogAsync();
            }
            finally { IsBusy = false; }
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
            var dtos = await _apiClient.GetAllCustomersAsync();

            var customers = dtos.Where(c => c.IsActive == true).Select(c => new CustomerListItemViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Age = c.Age,
                Address = c.Address
            }).ToList();

            Customers = new ObservableCollection<CustomerListItemViewModel>(customers);
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

    private bool CanSave(object? _) => !IsBusy && SelectedCustomer != null;

    private bool CanDelete(object? _) => !IsBusy && SelectedCustomer != null && !SelectedCustomer.IsNewCustomer;

    private void RefreshCommandStates()
    {
        ((RelayCommand<object>)SaveCustomerCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)DeleteCustomerCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)NewCustomerCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)RefreshCommand).NotifyCanExecuteChanged();
    }

    #endregion
}