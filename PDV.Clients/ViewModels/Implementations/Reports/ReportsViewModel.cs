using Microsoft.Win32;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Interfaces;
using PDV.Domain;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels.Implementations.Reports
{
    public class ReportsViewModel : Notifier, IReportsViewModel
    {
        private readonly IApiClient _apiClient;

        private DateTime _startDate;
        private DateTime _endDate;
        private decimal _totalRevenue;
        private int _totalSalesCount;
        private decimal _averageTicket;
        private bool _isBusy;
        private ObservableCollection<SaleResultDto> _reportItems = new();

        #region Properties

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); }
        }

        public decimal TotalRevenue
        {
            get => _totalRevenue;
            private set { _totalRevenue = value; OnPropertyChanged(); }
        }

        public int TotalSalesCount
        {
            get => _totalSalesCount;
            private set { _totalSalesCount = value; OnPropertyChanged(); }
        }

        public decimal AverageTicket
        {
            get => _averageTicket;
            private set { _averageTicket = value; OnPropertyChanged(); }
        }

        public ObservableCollection<SaleResultDto> ReportItems
        {
            get => _reportItems;
            private set { _reportItems = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ((RelayCommand<object>)GenerateReportCommand).NotifyCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand GenerateReportCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand BackCommand { get; }

        public event Action? RequestClose;

        #endregion

        public ReportsViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = DateTime.Now;

            GenerateReportCommand = new RelayCommand<object>(OnGenerateReport, _ => !IsBusy);
            ExportCommand = new RelayCommand<object>(OnExport, _ => !IsBusy && ReportItems.Count > 0);
            BackCommand = new RelayCommand<object>(OnBack);
        }

        #region Command Handlers

        private async void OnGenerateReport(object? _)
        {
            if (StartDate > EndDate)
            {
                var msg = new MessageBox
                {
                    Title = "Data Inválida",
                    Content = "A Data Inicial não pode ser maior que a Data Final.",
                    CloseButtonText = "OK"
                };
                await msg.ShowDialogAsync();
                return;
            }

            IsBusy = true;

            ReportItems.Clear();
            ResetKpis();

            try
            {
                var sales = await _apiClient.GetSalesByDateRangeAsync(StartDate, EndDate);

                if (sales != null && sales.Any())
                {
                    var orderedSales = sales.OrderByDescending(x => x.SaleDate).ToList();

                    ReportItems = new ObservableCollection<SaleResultDto>(orderedSales);

                    CalculateKpis();
                }
                else
                {
                    var msg = new MessageBox
                    {
                        Title = "Informação",
                        Content = "Nenhuma venda encontrada para o período selecionado.",
                        CloseButtonText = "OK"
                    };
                    await msg.ShowDialogAsync();
                }
            }
            catch (Exception ex)
            {
                var errorMsg = new MessageBox
                {
                    Title = "Erro",
                    Content = $"Falha ao gerar relatório: {ex.Message}",
                    CloseButtonText = "OK"
                };
                await errorMsg.ShowDialogAsync();
            }
            finally
            {
                IsBusy = false;
                ((RelayCommand<object>)ExportCommand).NotifyCanExecuteChanged();
            }
        }

        private async void OnExport(object? _)
        {
            if (ReportItems == null || !ReportItems.Any())
            {
                var alert = new MessageBox { Title = "Atenção", Content = "Não há dados para exportar.", CloseButtonText = "OK" };
                await alert.ShowDialogAsync();
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Arquivo CSV (*.csv)|*.csv",
                FileName = $"Relatorio_Vendas_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                Title = "Exportar Relatório"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                IsBusy = true;
                try
                {
                    var sb = new StringBuilder();

                    sb.AppendLine("Data;Cliente;Status;Valor Total");

                    foreach (var item in ReportItems)
                    {
                        string data = item.SaleDate.ToString("dd/MM/yyyy HH:mm");
                        string cliente = item.CustomerName?.Replace(";", "") ?? "N/A";
                        string status = item.Status ?? "N/A";
                        string valor = item.TotalAmount.ToString("F2");

                        sb.AppendLine($"{data};{cliente};{status};{valor}");
                    }

                    await File.WriteAllTextAsync(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);

                    var successMsg = new MessageBox { Title = "Sucesso", Content = "Arquivo exportado com sucesso!", CloseButtonText = "OK" };
                    await successMsg.ShowDialogAsync();
                }
                catch (Exception ex)
                {
                    var errorMsg = new MessageBox { Title = "Erro", Content = $"Falha ao exportar: {ex.Message}", CloseButtonText = "OK" };
                    await errorMsg.ShowDialogAsync();
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        private void OnBack(object? _)
        {
            RequestClose?.Invoke();
        }

        #endregion

        #region Helpers

        private void CalculateKpis()
        {
            TotalRevenue = ReportItems.Sum(x => x.TotalAmount);

            TotalSalesCount = ReportItems.Count;

            if (TotalSalesCount > 0)
            {
                AverageTicket = TotalRevenue / TotalSalesCount;
            }
            else
            {
                AverageTicket = 0;
            }
        }

        private void ResetKpis()
        {
            TotalRevenue = 0;
            TotalSalesCount = 0;
            AverageTicket = 0;
        }

        #endregion
    }
}