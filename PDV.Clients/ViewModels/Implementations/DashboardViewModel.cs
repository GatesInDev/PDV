using PDV.Clients.Models.Dashboard;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PDV.Clients.ViewModels.Interfaces;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels.Implementations
{
    public class DashboardViewModel : Notifier, IDashboardViewModel
    {
        private readonly IApiClient _dashboardService;

        public int TotalUsers
        {
            get => _totalUsers;
            set { _totalUsers = value; OnPropertyChanged(); }
        }
        private int _totalUsers;

        public int ActiveSessions
        {
            get => _activeSessions;
            set { _activeSessions = value; OnPropertyChanged(); }
        }
        private int _activeSessions;

        public int TransactionsToday
        {
            get => _transactionsToday;
            set { _transactionsToday = value; OnPropertyChanged(); }
        }
        private int _transactionsToday;

        public decimal RevenueToday
        {
            get => _revenueToday;
            set { _revenueToday = value; OnPropertyChanged(); }
        }
        private decimal _revenueToday;

        public string ServerStatus
        {
            get => _serverStatus;
            set { _serverStatus = value; OnPropertyChanged(); }
        }
        private string _serverStatus = "Desconectado";

        public double CpuLoad
        {
            get => _cpuLoad;
            set { _cpuLoad = value; OnPropertyChanged(); }
        }
        private double _cpuLoad;

        public ObservableCollection<TransactionModel> RecentTransactions { get; } = [];

        public ICommand RefreshCommand { get; }
        public ICommand NewSaleCommand { get; }
        public ICommand NewUserCommand { get; }
        public ICommand NewProductCommand { get; }

        private bool _isBusy;

        public DashboardViewModel(IApiClient dashboardService)
        {
            _dashboardService = dashboardService;
            RefreshCommand = new RelayCommand<object>(Refresh);
            NewSaleCommand = new RelayCommand<object>(NewSale);
            NewUserCommand = new RelayCommand<object>(NewUser);
            NewProductCommand = new RelayCommand<object>(NewProduct);

            _ = LoadInitialAsync();
        }

        private async Task LoadInitialAsync()
        {
            await RefreshAsync();
        }

        private void Refresh(object? _)
        {
            _ = RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            if (_isBusy) return;
            _isBusy = true;

            try
            {
                var life = await _dashboardService.GetLife();
                var summary = await _dashboardService.GetSummaryAsync();
                if (summary != null)
                {
                    TotalUsers = 0;
                    ActiveSessions = 0;
                    TransactionsToday = summary.DailySales;
                    RevenueToday = summary.DailyIncome;
                    ServerStatus = life.Status;
                    CpuLoad = life.CpuLoad;
                }

                var transactions = await _dashboardService.GetRecentTransactionsAsync(20);
                await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    RecentTransactions.Clear();
                    if (transactions != null)
                    {
                        foreach (var t in transactions.OrderByDescending(x => x.OccurredAt))
                        {
                            RecentTransactions.Add(t);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ServerStatus = $"Erro: {ex.Message}";
            }
            finally
            {
                _isBusy = false;
            }
        }

        private async void NewSale(object? _)
        {
            try
            {
                var cashVm = new CashViewModel(_dashboardService);

                var cashWindow = new CashView(cashVm);

                cashVm.RequestClose += () =>
                {
                    cashWindow.Close();
                };

                cashWindow.Show();
            }
            catch (Exception ex)
            {
                var msgBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Info",
                    Content = "Valor inválido.",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }


        private void NewUser(object? _)
        {
            // TODO: implementar fluxo de criação de usuário.
        }

        private async void NewProduct(object? _)
        {
            try
            {
                var productVm = new ProductViewModel(_dashboardService);

                var productWindow = new ProductView(productVm);

                productVm.RequestClose += () =>
                {
                    productWindow.Close();
                };

                productWindow.Show();
            }
            catch (Exception ex)
            {
                var msgBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Info",
                    Content = "Valor inválido.",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }
    }
}