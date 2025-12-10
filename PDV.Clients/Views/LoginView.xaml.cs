using PDV.Clients.ViewModels.Implementations;
using PDV.Clients.ViewModels.Interfaces;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views
{
    public partial class LoginView : FluentWindow
    {
        public string? Role { get; private set; } 

        public LoginView(ILoginViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;

            viewModel.LoginData += (loginData) =>
            {
                this.Role = loginData.Item2;
                this.DialogResult = loginData.Item1;
                this.Close();
            };
        }
    }
}
