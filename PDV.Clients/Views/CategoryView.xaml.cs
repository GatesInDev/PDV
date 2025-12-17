using PDV.Clients.ViewModels.Implementations.Category;
using PDV.Clients.ViewModels.Implementations.Customer;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views;

public partial class CategoryView : FluentWindow
{
    public CategoryView(CategoryViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

        viewModel.RequestClose += () =>
        {
            Close();
        };
    }
}