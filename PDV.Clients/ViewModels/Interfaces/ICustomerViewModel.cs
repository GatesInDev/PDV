using PDV.Clients.ViewModels.Implementations.Customer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PDV.Clients.ViewModels.Interfaces;

public interface ICustomerViewModel
{
    ObservableCollection<CustomerListItemViewModel> Customers { get; }
    CustomerListItemViewModel? SelectedListItem { get; set; }
    CustomerDetailViewModel? SelectedCustomer { get; set; }

    bool IsBusy { get; set; }
    string? ErrorMessage { get; set; }

    ICommand LoadCommand { get; }
    ICommand RefreshCommand { get; }
    ICommand NewCustomerCommand { get; }
    ICommand SaveCustomerCommand { get; }
    ICommand DeleteCustomerCommand { get; }
    ICommand BackCommand { get; }

    event Action? RequestClose;
}