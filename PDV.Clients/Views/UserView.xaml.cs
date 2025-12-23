using PDV.Clients.ViewModels.Implementations.User;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views;

public partial class UserView : FluentWindow
{
    public UserView(UserViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

        viewModel.RequestClose += () =>
        {
            Close();
        };
    }
}