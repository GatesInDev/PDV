using PDV.Clients.ViewModels;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views
{
    /// <summary>
    /// Interação lógica para CashView.xam
    /// </summary>
    public partial class CashView : FluentWindow
    {
        public CashView(CashViewModel view)
        {
            InitializeComponent();
            this.DataContext = view;
        }
    }
}
