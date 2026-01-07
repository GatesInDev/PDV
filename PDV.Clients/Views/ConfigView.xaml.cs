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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PDV.Clients.ViewModels.Interfaces;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views
{
    /// <summary>
    /// Interação lógica para ConfigView.xam
    /// </summary>
    public partial class ConfigView : FluentWindow
    {
        public ConfigView(IConfigViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.RequestClose += () =>
            {
                Close();
            };
        }
    }
}
