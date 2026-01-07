using PDV.Application.DTOs.Cash;
using PDV.Application.DTOs.Sales;
using PDV.Clients.Models;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
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
            string formatted = ApplyCurrencyMask(value);

            if (_discountValue != formatted)
            {
                _discountValue = formatted;
                OnPropertyChanged();
                RecalculateTotals(); 
            }
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
            string formatted = ApplyCurrencyMask(value);

            if (_openingAmountText != formatted)
            {
                _openingAmountText = formatted;
                OnPropertyChanged();
            }
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
            string searchTerm = SearchProductText?.Trim() ?? string.Empty;

            if (searchTerm.Length < 2)
            {
                ErrorMessage = "Digite pelo menos 2 caracteres para buscar o produto.";
                return;
            }

            string qtyInput = QuantityText?.Trim() ?? "1";

            if (!int.TryParse(qtyInput, out int quantity))
            {
                ErrorMessage = "A quantidade informada não é um número válido.";
                return;
            }

            if (quantity <= 0)
            {
                ErrorMessage = "A quantidade deve ser maior que zero.";
                return;
            }

            if (quantity > 1000)
            {
                ErrorMessage = "Quantidade muito alta para uma única inserção (Máx: 1000).";
                return;
            }

            var productsList = await _apiClient.GetProductsByNameAsync(searchTerm);
            var product = productsList?.FirstOrDefault();

            if (product != null)
            {
                if (!product.IsActive)
                {
                    ErrorMessage = "Este produto está inativo e não pode ser vendido.";
                    return;
                }

                var existingItem = CartItems.FirstOrDefault(x => x.ProductId == product.Id.ToString());

                if (existingItem != null)
                {
                    if (existingItem.Quantity + quantity > 10000)
                    {
                        ErrorMessage = "Limite de quantidade para este item no carrinho atingido.";
                        return;
                    }

                    existingItem.Quantity += quantity;
                    var index = CartItems.IndexOf(existingItem);
                    CartItems[index] = existingItem;
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
        string discountString = DiscountValue?.Replace("R$", "").Replace(" ", "").Trim() ?? "0";

        if (decimal.TryParse(discountString, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal d))
        {
            discount = d;
        }

        if (discount < 0)
        {
            ErrorMessage = "O desconto não pode ser negativo.";
            discount = 0; 
        }

        if (discount > SubTotal)
        {
            ErrorMessage = $"Desconto inválido. O máximo permitido é {SubTotal:C2}.";
            discount = SubTotal;
        }
        else if (discount > 0 && SubTotal > 0 && (discount / SubTotal) > 0.5m)
        {
            ErrorMessage = "Atenção: Desconto aplicado superior a 50%.";
        }
        else if (string.IsNullOrEmpty(ErrorMessage) || ErrorMessage.Contains("Desconto"))
        {
            ErrorMessage = null;
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
                ErrorMessage = "Selecione um cliente válido para prosseguir.";
                return;
            }

            if (CartItems.Count == 0)
            {
                ErrorMessage = "O carrinho está vazio.";
                return;
            }

            if (FinalTotal < 0)
            {
                ErrorMessage = "Erro crítico: O total da venda não pode ser negativo.";
                RecalculateTotals();
                return;
            }

            var validMethods = new[] { "Dinheiro", "Cartão de Crédito", "Cartão de Débito", "PIX" };
            if (string.IsNullOrEmpty(SelectedPaymentMethod) || !validMethods.Contains(SelectedPaymentMethod))
            {
                ErrorMessage = "Método de pagamento inválido.";
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
            var results = (await _apiClient.GetCustomersByNameAsync(query)).Where(w => w.IsActive == true);

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
            var results = (await _apiClient.GetProductsByNameAsync(query)).Where(w => w.IsActive == true);

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

        string amountStr = OpeningAmountText?.Replace("R$", "").Trim() ?? "0";

        if (!decimal.TryParse(amountStr, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal amount))
        {
            ErrorMessage = "Valor de abertura inválido.";
            return;
        }

        if (amount < 0)
        {
            ErrorMessage = "O valor de abertura não pode ser negativo.";
            return;
        }

        if (amount > 5000)
        {
            ErrorMessage = "Valor de abertura suspeito (Muito alto). Verifique a pontuação (vírgula).";
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
                ErrorMessage = "Falha ao abrir o caixa. Tente novamente.";
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

    private string ApplyCurrencyMask(string? input)
    {
        string digitsOnly = Regex.Replace(input ?? "", "[^0-9]", "");

        if (string.IsNullOrWhiteSpace(digitsOnly))
            digitsOnly = "0";

        if (long.TryParse(digitsOnly, out long cents))
        {
            decimal value = cents / 100m;

            return value.ToString("C2", new CultureInfo("pt-BR"));
        }

        return "red";
    }
}