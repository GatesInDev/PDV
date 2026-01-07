using PDV.Clients.Models.Dashboard;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Implementations.Category;
using PDV.Clients.ViewModels.Implementations.Customer;
using PDV.Clients.ViewModels.Implementations.Product;
using PDV.Clients.ViewModels.Implementations.User;
using PDV.Clients.ViewModels.Interfaces;
using PDV.Clients.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PDV.Clients.ViewModels.Implementations.Reports;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels.Implementations
{
    public class DashboardViewModel : Notifier, IDashboardViewModel
    {
        private readonly IApiClient _dashboardService;
        private readonly IAuthenticationService _authService;

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
        public ICommand NewCustomerCommand { get; }
        public ICommand NewCategoryCommand { get; }
        public ICommand NewProductCommand { get; }
        public ICommand NewReportCommand { get; }
        public ICommand ConfigCommand { get; }

        private bool _isBusy;

        public DashboardViewModel(IApiClient dashboardService, IAuthenticationService authService)
        {
            _dashboardService = dashboardService;
            _authService = authService;
            RefreshCommand = new RelayCommand<object>(Refresh);
            NewSaleCommand = new RelayCommand<object>(NewSale);
            NewUserCommand = new RelayCommand<object>(NewUser);
            NewCustomerCommand = new RelayCommand<object>(NewCustomer);
            NewProductCommand = new RelayCommand<object>(NewProduct);
            NewCategoryCommand = new RelayCommand<object>(NewCategory);
            NewReportCommand = new RelayCommand<object>(NewReport);
            ConfigCommand = new RelayCommand<object>(Config);

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
                var cashSessions = await _dashboardService.GetCashSessionsAsync();
                var summary = await _dashboardService.GetSummaryAsync();
                var users = await _dashboardService.GetAllUsersAsync();
                if (summary != null)
                {
                    TotalUsers = users.Count;
                    ActiveSessions = cashSessions.Count;
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

                var cashVm = new CashViewModel(_dashboardService, _authService, hasDashboardAccess: true);

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
                    Title = "Erro",
                    Content = $"Erro ao abrir Nova Venda: {ex.Message}",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }

        private async void NewCustomer(object? _)
        {
            try
            {
                var customerVm = new CustomerViewModel(_dashboardService);

                var customerWindow = new CustomerView(customerVm);

                customerVm.RequestClose += () =>
                {
                    customerWindow.Close();
                };

                customerWindow.Show();
            }
            catch (Exception ex)
            {
                var msgBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Erro",
                    Content = $"Erro ao abrir Clientes: {ex.Message}",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }

        private async void NewCategory(object? _)
        {
            try
            {
                var categoryVm = new CategoryViewModel(_dashboardService, hasDashboardAccess: true);

                var categoryWindow = new CategoryView(categoryVm);

                categoryVm.RequestClose += () =>
                {
                    categoryWindow.Close();
                };

                categoryWindow.Show();
            }
            catch (Exception ex)
            {
                var msgBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Erro",
                    Content = $"Erro ao abrir Categorias: {ex.Message}",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }

        private async void NewReport(object? _)
        {
            try
            {
                var reportVm = new ReportsViewModel(_dashboardService);

                var reportWindow = new ReportsWindow(reportVm);

                reportVm.RequestClose += () =>
                {
                    reportWindow.Close();
                };

                reportWindow.Show();
            }
            catch (Exception ex)
            {
                var msgBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Erro",
                    Content = $"Erro ao abrir Relatórios: {ex.Message}",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }

        private async void Config(object? _)
        {
            try
            {
                var configVm = new ConfigViewModel(_dashboardService);

                var reportWindow = new ConfigView(configVm);

                configVm.RequestClose += () =>
                {
                    reportWindow.Close();
                };

                reportWindow.Show();
            }
            catch (Exception ex)
            {
                var msgBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Erro",
                    Content = $"Erro ao abrir Configurações: {ex.Message}",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }

        private async void NewUser(object? _)
        {
            try
            {
                var userVm = new UserViewModel(_dashboardService, hasDashboardAccess: true);

                var userWindow = new UserView(userVm);

                userVm.RequestClose += () =>
                {
                    userWindow.Close();
                };

                userWindow.Show();
            }
            catch (Exception ex)
            {
                var msgBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Erro",
                    Content = $"Erro ao abrir Usuários: {ex.Message}",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
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
                    Title = "Erro",
                    Content = $"Erro ao abrir Produtos: {ex.Message}",
                    CloseButtonText = "OK",
                    MaxWidth = 400
                };
                await msgBox.ShowDialogAsync();
            }
        }
    }
}