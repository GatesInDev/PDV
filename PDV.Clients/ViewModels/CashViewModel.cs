using PDV.Application.DTOs.Cash;
using PDV.Application.DTOs.Product;
using PDV.Application.DTOs.Sales;
using PDV.Clients.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels;

public class CustomerSuggestionDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public override string ToString() => Name;
}
public class ProductsSuggestionDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    private decimal Price { get; set; }
    public override string ToString() => Name;
}

public class CartItemModel
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
}

public class CashViewModel : Notifier
{
    private readonly IApiClient _apiClient;

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
        set { _isOpenCashModalVisible = value; OnPropertyChanged(); }
    }

    public string OpeningAmountText
    {
        get => _openingAmountText;
        set { _openingAmountText = value; OnPropertyChanged(); }
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

    public CashViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;

        AddProductCommand = new RelayCommand<object>(OnAddProduct, CanAddProduct);
        RemoveItemCommand = new RelayCommand<object>(OnRemoveItem);
        FinalizeSaleCommand = new RelayCommand<object>(OnFinalizeSale, CanFinalizeSale);
        CancelSaleCommand = new RelayCommand<object>(OnCancelSale);
        BackCommand = new RelayCommand<object>(OnBack);
        SelectCustomerCommand = new RelayCommand<object>(OnSelectCustomer);
        SelectProductCommand = new RelayCommand<object>(OnSelectProduct);
        OpenCashCommand = new RelayCommand<object>(OnOpenCash);
        CloseCashCommand = new RelayCommand<object>(OnCloseCash);

        CheckCashStatus();
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
                var msgBox = new MessageBox
                {
                    Title = "Aviso",
                    Content = "Quantidade inválida.",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
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
        }
    }

    private void RecalculateTotals()
    {
        SubTotal = CartItems.Sum(i => i.TotalPrice);
        decimal discount = 0;
        if (decimal.TryParse(DiscountValue?.Replace("R$", "").Trim(), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal d))
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
        try
        {
            if (string.IsNullOrEmpty(SelectedPaymentMethod))
            {
                var msgBox = new MessageBox
                {
                    Title = "Aviso",
                    Content = "Selecione um método de pagamento.",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
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
                var msgBoxSuccess = new MessageBox
                {
                    Title = "Sucesso",
                    Content = "Venda criada com sucesso!",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };

                await msgBoxSuccess.ShowDialogAsync();
                OnCancelSale(null);
            }
            else
            {
                var msgBoxError = new MessageBox
                {
                    Title = "Erro",
                    Content = "Não foi possível criar a venda.",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBoxError.ShowDialogAsync();
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
        }
    }

    private void OnSelectProduct(object? parameter)
    {
        if (parameter is AutoSuggestBoxSuggestionChosenEventArgs args &&
            args.SelectedItem is ProductsSuggestionDTO selected)
        {
            SearchProductText = selected.Name;
            ProductSuggestions.Clear();
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
        try
        {
            var sessions = await _apiClient.GetCashSessionsAsync();

            var activeSession = sessions.FirstOrDefault(x => x.ClosedAt == null);

            if (activeSession == null)
            {
                IsOpenCashModalVisible = true;
                _currentSessionId = null;
            }
            else
            {
                IsOpenCashModalVisible = false;
                _currentSessionId = activeSession.Id;
            }
        }
        catch (Exception ex)
        {
            var msgBox = new MessageBox
            {
                Title = "Erro",
                Content = $"Erro ao verificar caixa: {ex.Message}.",
                CloseButtonText = "OK",
                MaxWidth = 400
            };
            await msgBox.ShowDialogAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void OnOpenCash(object? _)
    {
        if (!decimal.TryParse(OpeningAmountText.Replace("R$", "").Trim(), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal amount))
        {
            var msgBox = new MessageBox
            {
                Title = "Info",
                Content = "Valor inválido.",
                CloseButtonText = "OK",
                MaxWidth = 400
            };
            await msgBox.ShowDialogAsync();
            return;
        }

        IsBusy = true;
        try
        {
            var dto = new OpenCashSessionDTO { OpeningAmount = amount };
            bool success = await _apiClient.OpenCashSessionAsync(dto);

            if (success)
            {
                var msgBox = new MessageBox
                {
                    Title = "Info",
                    Content = "Caixa aberto com sucesso.",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
                IsOpenCashModalVisible = false;
                CheckCashStatus();
            }
            else
            {
                var msgBox = new MessageBox
                {
                    Title = "Info",
                    Content = "Falha ao abrir o caixa.",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }
        finally { IsBusy = false; }
    }

    private async void OnCloseCash(object? _)
    {
        if (_currentSessionId == null)
        {
            var msgBox = new MessageBox
            {
                Title = "Info",
                Content = "Não há sessão aberta para fechar.",
                CloseButtonText = "OK",
                MaxWidth = 400
            };
            await msgBox.ShowDialogAsync();
            return;
        }

        var confirmBox = new MessageBox
        {
            Title = "Confirmação",
            Content = "Tem certeza que deseja fechar o caixa?",
            PrimaryButtonText = "Sim",
            CloseButtonText = "Não"
        };

        var result = await confirmBox.ShowDialogAsync();
        if (result == MessageBoxResult.Primary)
        {
            IsBusy = true;
            try
            {
                var dto = new CloseCashSessionDTO { Id = _currentSessionId.Value };
                bool success = await _apiClient.CloseCashSessionAsync(dto);

                if (success)
                {
                    var msgBox = new MessageBox
                    {
                        Title = "Info",
                        Content = "Caixa fechado.",
                        CloseButtonText = "OK",
                        MaxWidth = 400
                    };
                    await msgBox.ShowDialogAsync();
                    OnCancelSale(null);
                    CheckCashStatus();
                }
                else
                {
                    var msgBox = new MessageBox
                    {
                        Title = "Info",
                        Content = "Erro ao fechar o caixa.",
                        CloseButtonText = "OK",
                        MaxWidth = 400
                    };
                    await msgBox.ShowDialogAsync();
                }
            }
            finally { IsBusy = false; }
        }
    }
}