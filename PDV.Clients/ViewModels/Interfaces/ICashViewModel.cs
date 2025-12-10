using PDV.Clients.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface ICashViewModel
    {
        ObservableCollection<CartItemModel> CartItems { get; }
        ObservableCollection<CustomerSuggestionDTO> CustomerSuggestions { get; }
        ObservableCollection<ProductsSuggestionDTO> ProductSuggestions { get; }

        string? SearchProductText { get; set; }
        string QuantityText { get; set; }
        string? CustomerSearchText { get; set; }
        string? SelectedCustomerName { get; set; }
        string SelectedPaymentMethod { get; set; }
        string DiscountValue { get; set; }

        decimal SubTotal { get; set; }
        decimal FinalTotal { get; set; }

        bool IsBusy { get; set; }
        bool IsOpenCashModalVisible { get; set; }
        string OpeningAmountText { get; set; }
        string? ErrorMessage { get; set; }

        ICommand AddProductCommand { get; }
        ICommand RemoveItemCommand { get; }
        ICommand FinalizeSaleCommand { get; }
        ICommand SelectCustomerCommand { get; }
        ICommand SelectProductCommand { get; }
        ICommand CancelSaleCommand { get; }
        ICommand BackCommand { get; }
        ICommand OpenCashCommand { get; }
        ICommand CloseCashCommand { get; }

        event Action? RequestClose;
    }
}