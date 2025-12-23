using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PDV.Clients.ViewModels.Interfaces;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views
{
    /// <summary>
    /// Lógica interna para ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : FluentWindow
    {
        public ReportsWindow(IReportsViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
