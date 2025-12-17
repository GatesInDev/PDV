using PDV.Application.DTOs.Cash;
using PDV.Application.DTOs.Product;
using PDV.Application.DTOs.Sales;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using PDV.Clients.ViewModels.Interfaces;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels.Implementations;

public class CashViewModel : Notifier, ICashViewModel
{
    private readonly IApiClient _apiClient;
    private readonly IAuthenticationService _authService;

    private string? _searchProductText;
    private string _quantityText = "1";
    private string? _customerSearchText;
    private string? _selectedCustomerName;
    private Guid? _selectedCustomerId;
    private string _selectedPaymentMethod = "Dinheiro";
    private string _discountValue = "0,00";
    private decimal _subTotal;
    private decimal _finalTotal;
    private bool _isBusy;
    private bool _isOpenCashModalVisible;
    private string _openingAmountText = "0,00";
    private Guid? _currentSessionId;
    private string? _errorMessage;
    private bool _hasDashboardAccess;
    private string _backButtonText = "Voltar";

    public ObservableCollection<CartItemModel> CartItems { get; } = new();
    public ObservableCollection<CustomerSuggestionDTO> CustomerSuggestions { get; } = new();
    public ObservableCollection<ProductsSuggestionDTO> ProductSuggestions { get; } = new();

    public string? SearchProductText
    {
        get => _searchProductText;
        set
        {
            _searchProductText = value;
            OnPropertyChanged();
            SearchProductAuto(value);
            ((RelayCommand<object>)AddProductCommand).NotifyCanExecuteChanged();
        }
    }

    public string QuantityText
    {
        get => _quantityText;
        set
        {
            _quantityText = value;
            OnPropertyChanged();
        }
    }

    public string? CustomerSearchText
    {
        get => _customerSearchText;
        set
        {
            _customerSearchText = value;
            OnPropertyChanged();
            SearchCustomersAuto(value);
        }
    }

    public string? SelectedCustomerName
    {
        get => _selectedCustomerName;
        set
        {
            _selectedCustomerName = value;
            OnPropertyChanged();
        }
    }

    public string SelectedPaymentMethod
    {
        get => _selectedPaymentMethod;
        set
        {
            _selectedPaymentMethod = value;
            OnPropertyChanged();
        }
    }

    public string DiscountValue
    {
        get => _discountValue;
        set
        {
            _discountValue = value;
            OnPropertyChanged();
            RecalculateTotals();
        }
    }

    public decimal SubTotal
    {
        get => _subTotal;
        set
        {
            _subTotal = value;
            OnPropertyChanged();
        }
    }

    public decimal FinalTotal
    {
        get => _finalTotal;
        set
        {
            _finalTotal = value;
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
            ((RelayCommand<object>)AddProductCommand).NotifyCanExecuteChanged();
            ((RelayCommand<object>)FinalizeSaleCommand).NotifyCanExecuteChanged();
        }
    }

    public bool IsOpenCashModalVisible
    {
        get => _isOpenCashModalVisible;
        set
        {
            _isOpenCashModalVisible = value;
            OnPropertyChanged();
        }
    }

    public string OpeningAmountText
    {
        get => _openingAmountText;
        set
        {
            _openingAmountText = value;
            OnPropertyChanged();
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
    public string OpenCashButtonText
    {
        get => _openCashButtonText;
        set
        {
            _openCashButtonText = value;
            OnPropertyChanged();
        }
    }
    private string _openCashButtonText = "Abrir Caixa e Iniciar Vendas";

    public ICommand AddProductCommand { get; }
    public ICommand RemoveItemCommand { get; }
    public ICommand FinalizeSaleCommand { get; }
    public ICommand SelectCustomerCommand { get; }
    public ICommand SelectProductCommand { get; }
    public ICommand CancelSaleCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand OpenCashCommand { get; }
    public ICommand CloseCashCommand { get; }

    public event Action? RequestClose;

    public CashViewModel(IApiClient apiClient, IAuthenticationService authService, bool hasDashboardAccess = true)
    {
        _apiClient = apiClient;
        _authService = authService;
        _hasDashboardAccess = hasDashboardAccess;

        AddProductCommand = new RelayCommand<object>(OnAddProduct, CanAddProduct);
        RemoveItemCommand = new RelayCommand<object>(OnRemoveItem);
        FinalizeSaleCommand = new RelayCommand<object>(OnFinalizeSale, CanFinalizeSale);
        CancelSaleCommand = new RelayCommand<object>(OnCancelSale);
        BackCommand = new RelayCommand<object>(OnBack);
        SelectCustomerCommand = new RelayCommand<object>(OnSelectCustomer);
        SelectProductCommand = new RelayCommand<object>(OnSelectProduct);
        OpenCashCommand = new RelayCommand<object>(OnOpenCash);
        CloseCashCommand = new RelayCommand<object>(OnCloseCash);

        UpdateBackButtonText();
        CheckCashStatus();
    }

    private void UpdateBackButtonText()
    {
        BackButtonText = _hasDashboardAccess ? "Voltar" : "Fechar";
        OpenCashButtonText = _hasDashboardAccess ? "Abrir Caixa e Iniciar Vendas" : "Fechar Aplicação";
    }

    private bool CanAddProduct(object? _) => !IsBusy && !string.IsNullOrWhiteSpace(SearchProductText);

    private async void OnAddProduct(object? _)
    {
        IsBusy = true;
        ErrorMessage = null;

        try
        {
            if (!int.TryParse(QuantityText, out int quantity) || quantity <= 0)
            {
                ErrorMessage = "Quantidade inválida.";
                return;
            }

            var productsList = await _apiClient.GetProductsByNameAsync(SearchProductText);

            var product = productsList?.FirstOrDefault();

            if (product != null)
            {
                var existingItem = CartItems.FirstOrDefault(x => x.ProductId == product.Id.ToString());

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;

                    var index = CartItems.IndexOf(existingItem);
                    CartItems[index] = new CartItemModel
                    {
                        ProductId = existingItem.ProductId,
                        ProductName = existingItem.ProductName,
                        UnitPrice = existingItem.UnitPrice,
                        Quantity = existingItem.Quantity
                    };
                }
                else
                {
                    CartItems.Add(new CartItemModel
                    {
                        ProductId = product.Id.ToString(),
                        ProductName = product.Name,
                        UnitPrice = product.Price,
                        Quantity = quantity
                    });
                }

                SearchProductText = string.Empty;
                QuantityText = "1";
                RecalculateTotals();
                ErrorMessage = null;
            }
            else
            {
                ErrorMessage = "Produto não encontrado.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erro: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void OnRemoveItem(object? parameter)
    {
        if (parameter is CartItemModel item)
        {
            CartItems.Remove(item);
            RecalculateTotals();
            ErrorMessage = null;
        }
    }

    private void RecalculateTotals()
    {
        SubTotal = CartItems.Sum(i => i.TotalPrice);
        decimal discount = 0;
        if (decimal.TryParse(DiscountValue?.Replace("R$", "").Trim(), NumberStyles.Any, new CultureInfo("pt-BR"),
                out decimal d))
        {
            discount = d;
        }

        FinalTotal = SubTotal - discount;
        if (FinalTotal < 0) FinalTotal = 0;
        ((RelayCommand<object>)FinalizeSaleCommand).NotifyCanExecuteChanged();
    }

    private bool CanFinalizeSale(object? _) => !IsBusy && CartItems.Count > 0;

    private async void OnFinalizeSale(object? _)
    {
        IsBusy = true;
        ErrorMessage = null;

        try
        {
            if (_selectedCustomerId == null || _selectedCustomerId == Guid.Empty)
            {
                ErrorMessage = "Selecione um cliente para prosseguir.";
                return;
            }

            if (string.IsNullOrEmpty(SelectedPaymentMethod))
            {
                ErrorMessage = "Selecione um método de pagamento.";
                return;
            }

            var createSaleDto = new CreateSalesDTO
            {
                CustomerId = _selectedCustomerId,
                PaymentMethod = SelectedPaymentMethod,
                Products = CartItems.Select(i => new CreateSaleProductDTO
                {
                    ProductId = Guid.TryParse(i.ProductId, out var gId) ? gId : Guid.Empty,
                    Quantity = i.Quantity
                }).ToList()
            };

            if (await _apiClient.PostSaleAsync(createSaleDto, CancellationToken.None))
            {
                ErrorMessage = null;
                OnCancelSale(null);
            }
            else
            {
                ErrorMessage = "Não foi possível criar a venda.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erro ao finalizar venda: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void SearchCustomersAuto(string? query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 1)
        {
            CustomerSuggestions.Clear();
            return;
        }

        try
        {
            var results = await _apiClient.GetCustomersByNameAsync(query);

            CustomerSuggestions.Clear();

            foreach (var item in results)
            {
                CustomerSuggestions.Add(new CustomerSuggestionDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
        }
        catch
        {
            CustomerSuggestions.Clear();
        }
    }

    private async void SearchProductAuto(string? query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 1)
        {
            ProductSuggestions.Clear();
            return;
        }

        try
        {
            var results = await _apiClient.GetProductsByNameAsync(query);

            ProductSuggestions.Clear();

            foreach (var item in results)
            {
                ProductSuggestions.Add(new ProductsSuggestionDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
        }
        catch
        {
            ProductSuggestions.Clear();
        }
    }

    private void OnSelectCustomer(object? parameter)
    {
        if (parameter is AutoSuggestBoxSuggestionChosenEventArgs args &&
            args.SelectedItem is CustomerSuggestionDTO selected)
        {
            SelectedCustomerName = selected.Name;
            _selectedCustomerId = selected.Id;
            CustomerSearchText = selected.Name;
            CustomerSuggestions.Clear();
            ErrorMessage = null;
        }
    }

    private void OnSelectProduct(object? parameter)
    {
        if (parameter is AutoSuggestBoxSuggestionChosenEventArgs args &&
            args.SelectedItem is ProductsSuggestionDTO selected)
        {
            SearchProductText = selected.Name;
            ProductSuggestions.Clear();
            ErrorMessage = null;
        }
    }

    private void OnCancelSale(object? _)
    {
        CartItems.Clear();
        SearchProductText = string.Empty;
        QuantityText = "1";
        CustomerSearchText = string.Empty;
        SelectedCustomerName = null;
        _selectedCustomerId = null;
        DiscountValue = "0,00";
        SelectedPaymentMethod = "Dinheiro";
        RecalculateTotals();
        ErrorMessage = null;
    }

    private void OnBack(object? _)
    {
        RequestClose?.Invoke();
    }

    private async void CheckCashStatus()
    {
        IsBusy = true;
        ErrorMessage = null;

        try
        {
            var sessions = await _apiClient.GetCashSessionsAsync();

            var currentUsername = _authService.GetCurrentUsername();
            
            var activeSession = sessions.FirstOrDefault(x =>
                x.ClosedAt == null &&
                x.OperatorName == currentUsername);

            if (activeSession == null)
            {
                IsOpenCashModalVisible = true;
                _currentSessionId = null;
            }
            else
            {
                IsOpenCashModalVisible = false;
                _currentSessionId = activeSession.Id;
                ErrorMessage = null;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erro ao verificar caixa: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void OnOpenCash(object? _)
    {
        ErrorMessage = null;

        if (!decimal.TryParse(OpeningAmountText.Replace("R$", "").Trim(), NumberStyles.Any, new CultureInfo("pt-BR"),
                out decimal amount))
        {
            ErrorMessage = "Valor inválido.";
            return;
        }

        IsBusy = true;
        try
        {
            var dto = new OpenCashSessionDTO { OpeningAmount = amount };
            bool success = await _apiClient.OpenCashSessionAsync(dto);

            if (success)
            {
                ErrorMessage = null;
                OpeningAmountText = "0,00"; 
                await Task.Delay(500);
                CheckCashStatus(); 
            }
            else
            {
                ErrorMessage = "Falha ao abrir o caixa.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            
            if (ex.Message.Contains("já possui") || ex.Message.Contains("aberto"))
            {
                await Task.Delay(500);
                CheckCashStatus();
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void OnCloseCash(object? _)
    {
        ErrorMessage = null;

        if (_currentSessionId == null)
        {
            ErrorMessage = "Não há sessão aberta para fechar.";
            return;
        }

        IsBusy = true;
        try
        {
            var dto = new CloseCashSessionDTO { Id = _currentSessionId.Value };
            bool success = await _apiClient.CloseCashSessionAsync(dto);

            if (success)
            {
                ErrorMessage = null;
                OnCancelSale(null);
                CheckCashStatus();
            }
            else
            {
                ErrorMessage = "Erro ao fechar o caixa.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erro ao fechar caixa: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}