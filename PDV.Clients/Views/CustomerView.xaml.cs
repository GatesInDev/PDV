using PDV.Clients.ViewModels.Implementations.Customer;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views;

public partial class CustomerView : FluentWindow
{
    public CustomerView(CustomerViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

        viewModel.RequestClose += () =>
        {
            Close();
        };
    }
}