using System.ComponentModel;
using PDV.Clients.ViewModels.Implementations;
using PDV.Clients.ViewModels.Interfaces;
using Wpf.Ui.Controls;

namespace PDV.Clients.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Dashboard : FluentWindow
    {
        public Dashboard(IDashboardViewModel view)
        {
            InitializeComponent();
            this.DataContext = view;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

            base.OnClosing(e);
        }

    
    }
}