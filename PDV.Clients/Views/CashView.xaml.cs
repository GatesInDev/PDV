using PDV.Clients.ViewModels.Implementations;
using PDV.Clients.ViewModels.Interfaces;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views
{
    /// <summary>
    /// Interação lógica para CashView.xam
    /// </summary>
    public partial class CashView : FluentWindow
    {
        public CashView(ICashViewModel view)
        {
            InitializeComponent();
            this.DataContext = view;
        }
    }
}
