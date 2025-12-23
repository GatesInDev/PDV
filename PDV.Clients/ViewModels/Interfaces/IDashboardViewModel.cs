using System.Collections.ObjectModel;
using System.Windows.Input;
using PDV.Clients.Models.Dashboard;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface IDashboardViewModel
    {
        int TotalUsers { get; set; }
        int ActiveSessions { get; set; }
        int TransactionsToday { get; set; }
        decimal RevenueToday { get; set; }
        string ServerStatus { get; set; }
        double CpuLoad { get; set; }

        ObservableCollection<TransactionModel> RecentTransactions { get; }

        ICommand RefreshCommand { get; }
        ICommand NewSaleCommand { get; }
        ICommand NewUserCommand { get; }
        ICommand NewCustomerCommand { get; }
        ICommand NewCategoryCommand { get; }
        ICommand NewProductCommand { get; }
        ICommand NewReportCommand { get; }
    }
}