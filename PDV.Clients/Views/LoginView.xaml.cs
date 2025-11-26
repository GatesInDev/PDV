using PDV.Clients.ViewModels;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views
{
    public partial class LoginView : FluentWindow
    {
        public string? Role { get; private set; } 

        public LoginView(LoginViewModel viewModel)
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
