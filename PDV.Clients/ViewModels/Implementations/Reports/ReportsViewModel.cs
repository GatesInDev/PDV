using ClosedXML.Excel;
using Microsoft.Win32;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Interfaces;
using PDV.Domain;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
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
                ErrorMessage = "Data invalida.";
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
                    ErrorMessage = "Nenhuma venda encontrada para o período selecionado.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Falha ao gerar relatório: {ex.Message}";
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
                ErrorMessage = "Não há dados para exportar.";
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Pasta de Trabalho do Excel (*.xlsx)|*.xlsx",
                FileName = $"Relatorio_Vendas_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                Title = "Exportar Relatório Profissional"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                IsBusy = true;
                try
                {
                    var config = await _apiClient.GetConfigAsync();

                    using var workbook = new XLWorkbook();
                    var ws = workbook.Worksheets.Add("Vendas");

                    ws.Style.Font.FontName = "Calibri";
                    ws.Style.Font.FontSize = 11;

                    // ================= LOGO =================
                    if (config?.Logo?.Length > 0)
                    {
                        using var ms = new MemoryStream(config.Logo);
                        var image = ws.AddPicture(ms)
                                              .MoveTo(ws.Cell("A1"))
                                              .Scale(0.45);

                        image.Width = 100;
                        image.Height = 100;
                    }

                    // ================= CABEÇALHO =================
                    ws.Cell("C1").Value = config?.NomeFantasia ?? "PDV System";
                    ws.Cell("C1").Style
                        .Font.SetBold()
                        .Font.SetFontSize(20);

                    ws.Cell("C2").Value = "Relatório de Vendas";
                    ws.Cell("C2").Style.Font.FontSize = 14;

                    ws.Cell("C3").Value = $"Período: {StartDate:dd/MM/yyyy} até {EndDate:dd/MM/yyyy}";
                    ws.Cell("C3").Style.Font.FontColor = XLColor.Gray;

                    // Linha separadora
                    ws.Range("A5:D5").Merge();
                    ws.Cell("A5").Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    // ================= RESUMO =================
                    ws.Cell("A6").Value = "Total Vendido";
                    ws.Cell("B6").Value = TotalRevenue;
                    ws.Cell("B6").Style.NumberFormat.Format = "R$ #,##0.00";
                    ws.Cell("B6").Style.Font.SetBold().Font.SetFontColor(XLColor.DarkGreen);

                    ws.Cell("C6").Value = "Ticket Médio";
                    ws.Cell("D6").Value = AverageTicket;
                    ws.Cell("D6").Style.NumberFormat.Format = "R$ #,##0.00";
                    ws.Cell("D6").Style.Font.SetBold();

                    ws.Range("A6:D6").Style.Fill.BackgroundColor = XLColor.FromHtml("#F8F9FA");

                    // ================= TABELA =================
                    int row = 8;

                    ws.Cell(row, 1).Value = "Data";
                    ws.Cell(row, 2).Value = "Cliente";
                    ws.Cell(row, 3).Value = "Produtos";
                    ws.Cell(row, 4).Value = "Valor";

                    var header = ws.Range(row, 1, row, 4);
                    header.Style
                        .Font.SetBold()
                        .Font.SetFontColor(XLColor.White)
                        .Fill.SetBackgroundColor(XLColor.FromHtml("#343A40"))
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    row++;

                    foreach (var item in ReportItems)
                    {
                        ws.Cell(row, 1).Value = item.SaleDate;
                        ws.Cell(row, 1).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";

                        ws.Cell(row, 2).Value = item.CustomerName ?? "Consumidor Final";
                        ws.Cell(row, 3).Value = item.Product;

                        ws.Cell(row, 4).Value = item.TotalAmount;
                        ws.Cell(row, 4).Style.NumberFormat.Format = "R$ #,##0.00";

                        row++;
                    }

                    // Criar tabela com zebra
                    var tableRange = ws.Range(8, 1, row - 1, 4);
                    var table = tableRange.CreateTable();
                    table.Theme = XLTableTheme.TableStyleMedium9;

                    // Alinhamentos
                    ws.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    // Ajustes finais
                    ws.Columns().AdjustToContents();
                    ws.Column(2).Width += 5;

                    workbook.SaveAs(saveFileDialog.FileName);
                    ErrorMessage = null;
                    
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Falha ao exportar Excel: {ex.Message}";
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