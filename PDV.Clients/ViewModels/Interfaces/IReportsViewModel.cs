using System.Collections.ObjectModel;
using System.Windows.Input;
using PDV.Domain;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface IReportsViewModel
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }


        decimal TotalRevenue { get; }
        int TotalSalesCount { get; }
        decimal AverageTicket { get; }


        ObservableCollection<SaleResultDto> ReportItems { get; }


        bool IsBusy { get; }
        ICommand GenerateReportCommand { get; }
        ICommand ExportCommand { get; }
        ICommand BackCommand { get; }
    }
}