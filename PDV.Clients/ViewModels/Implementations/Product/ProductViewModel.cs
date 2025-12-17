using PDV.Application.DTOs.Category;
using PDV.Application.DTOs.Product;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf.Ui.Input;
using Wpf.Ui.Controls;
using System.Linq;

namespace PDV.Clients.ViewModels.Implementations.Product;

public class ProductViewModel : Notifier, IProductViewModel
{
    private readonly IApiClient _apiClient;

    private ObservableCollection<ProductListItemViewModel> _products = new();
    private ObservableCollection<CategoryDTO> _categories = new();

    private ProductListItemViewModel? _selectedListItem;
    private ProductDetailViewModel? _selectedProduct;

    private bool _isBusy;
    private string? _errorMessage;

    #region Properties

    public ObservableCollection<ProductListItemViewModel> Products
    {
        get => _products;
        private set { _products = value; OnPropertyChanged(); }
    }

    public ObservableCollection<CategoryDTO> Categories
    {
        get => _categories;
        private set { _categories = value; OnPropertyChanged(); }
    }

    public ProductListItemViewModel? SelectedListItem
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

    public ProductDetailViewModel? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
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
    public ICommand NewProductCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand SaveProductCommand { get; }
    public ICommand DeleteProductCommand { get; }

    public event Action? RequestClose;

    #endregion

    public ProductViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;

        LoadCommand = new RelayCommand<object>(OnLoad);
        RefreshCommand = new RelayCommand<object>(OnRefresh, _ => !IsBusy);
        NewProductCommand = new RelayCommand<object>(OnNewProduct, _ => !IsBusy);
        SaveProductCommand = new RelayCommand<object>(OnSave, CanSave);
        BackCommand = new RelayCommand<object>(OnBack);
        DeleteProductCommand = new RelayCommand<object>(OnDelete, CanDelete);

        OnLoad(null);
    }

    #region Command Handlers

    private async void OnLoad(object? _)
    {
        await LoadDataAsync();
    }

    private async void OnRefresh(object? _)
    {
        SelectedProduct = null;
        SelectedListItem = null;
        await LoadDataAsync();
    }

    private void OnNewProduct(object? _)
    {
        SelectedListItem = null;

        SelectedProduct = new ProductDetailViewModel
        {
            Id = Guid.Empty,
            Name = string.Empty,
            Sku = string.Empty,
            Price = 0,
            StockQuantity = 0,
            MetricUnit = "Un",
            CategoryId = 0,
            IsActive = true
        };
    }

    private void OnBack(object? _)
    {
        RequestClose?.Invoke();
    }

    private void LoadDetailFromList(ProductListItemViewModel item)
    {
        var catId = Categories.FirstOrDefault(c => c.Name == item.CategoryName)?.Id ?? 0;

        SelectedProduct = new ProductDetailViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Sku = item.Sku,
            Price = item.Price,
            StockQuantity = item.StockQuantity,
            MetricUnit = item.MetricUnit,
            IsActive = item.IsActive,
            CategoryId = catId
        };
    }

    private async void OnSave(object? _)
    {
        if (SelectedProduct == null) return;

        if (!SelectedProduct.IsValid)
        {
            ErrorMessage = "Preencha todos os campos obrigatórios (*).";
            return;
        }

        IsBusy = true;
        ErrorMessage = null;

        try
        {
            var createDto = new CreateProductDTO
            {
                Sku = SelectedProduct.Sku,
                Name = SelectedProduct.Name,
                Price = SelectedProduct.Price,
                Quantity = SelectedProduct.StockQuantity,
                MetricUnit = SelectedProduct.MetricUnit,
                CategoryId = SelectedProduct.CategoryId
            };

            var updateDto = new UpdateProductDTO
            {
                Sku = SelectedProduct.Sku,
                Name = SelectedProduct.Name,
                Price = SelectedProduct.Price,
                Quantity = SelectedProduct.StockQuantity,
                MetricUnit = SelectedProduct.MetricUnit,
                CategoryId = SelectedProduct.CategoryId
            };

            if (SelectedProduct.IsNewProduct)
            {
                await _apiClient.CreateProductAsync(createDto);
            }
            else
            {
                await _apiClient.UpdateProductAsync(SelectedProduct.Id, updateDto);
            }

            var msgBox = new MessageBox { Title = "Sucesso", Content = "Salvo com sucesso!", CloseButtonText = "OK" };
            await msgBox.ShowDialogAsync();

            SelectedProduct = null;
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
        if (SelectedProduct == null) return;

        var confirmBox = new MessageBox
        {
            Title = "Confirmação",
            Content = $"Excluir '{SelectedProduct.Name}'?",
            PrimaryButtonText = "Sim",
            CloseButtonText = "Não"
        };

        if (await confirmBox.ShowDialogAsync() == MessageBoxResult.Primary)
        {
            IsBusy = true;
            try
            {
                await _apiClient.DeleteProductAsync(SelectedProduct.Id);
                SelectedProduct = null;
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
            var cats = await _apiClient.GetAllCategoriesAsync();
            Categories = new ObservableCollection<CategoryDTO>(cats);

            var dtos = (await _apiClient.GetAllProductsAsync()).Where(p => p.IsActive == true);

            var tasks = dtos.Select(async p =>
            {
                var stockData = await _apiClient.GetStockFromIdAsync(p.Id);

                return new ProductListItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Sku = p.SKU,
                    Price = p.Price,
                    StockQuantity = stockData.Quantity,
                    MetricUnit = stockData.MetricUnit,
                    CategoryName = p.CategoryName ?? "N/A",
                    IsActive = true
                };
            });

            var results = await Task.WhenAll(tasks);

            Products = new ObservableCollection<ProductListItemViewModel>(results);
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

    private bool CanSave(object? _) => !IsBusy && SelectedProduct != null;

    private bool CanDelete(object? _) => !IsBusy && SelectedProduct != null && !SelectedProduct.IsNewProduct;

    private void RefreshCommandStates()
    {
        ((RelayCommand<object>)SaveProductCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)DeleteProductCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)NewProductCommand).NotifyCanExecuteChanged();
        ((RelayCommand<object>)RefreshCommand).NotifyCanExecuteChanged();
    }

    #endregion
}