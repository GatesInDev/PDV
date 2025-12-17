using System.Collections.ObjectModel;
using System.Windows.Input;
using PDV.Application.DTOs.Product;
using PDV.Application.DTOs.Category;
using PDV.Clients.ViewModels.Implementations.Product;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface IProductViewModel
    {
        ObservableCollection<ProductListItemViewModel> Products { get; }
        ProductDetailViewModel? SelectedProduct { get; set; }
        ObservableCollection<CategoryDTO> Categories { get; }
        
        bool IsBusy { get; set; }
        string? ErrorMessage { get; set; }
        
        ICommand LoadCommand { get; }
        ICommand NewProductCommand { get; }
        ICommand SaveProductCommand { get; }
        ICommand DeleteProductCommand { get; }
        ICommand RefreshCommand { get; }
    }
}
