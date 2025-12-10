using PDV.Clients.ViewModels.Interfaces;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views
{
    /// <summary>
    /// Interação lógica para ProductView.xam
    /// </summary>
    public partial class ProductView : FluentWindow
    {
        public ProductView(IProductViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
