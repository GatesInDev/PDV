using System.Collections.ObjectModel;
using System.Windows.Input;
using PDV.Clients.ViewModels.Implementations;
using PDV.Application.DTOs.Product;
using PDV.Application.DTOs.Category;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface IProductViewModel
    {
        // Master Grid
        ObservableCollection<ProductListItemViewModel> Products { get; }
        ProductDetailViewModel? SelectedProduct { get; set; }
        
        // Categories
        ObservableCollection<CategoryDTO> Categories { get; }
        
        // State
        bool IsBusy { get; set; }
        string? ErrorMessage { get; set; }
        
        // Commands
        ICommand LoadCommand { get; }
        ICommand NewProductCommand { get; }
        ICommand SaveProductCommand { get; }
        ICommand DeleteProductCommand { get; }
        ICommand RefreshCommand { get; }
    }
}
