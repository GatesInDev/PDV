using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PDV.Clients.Views;
using PDV.Clients.Services.Implementations;
using PDV.Clients.Services.Interfaces;
using Wpf.Ui.Controls;
using WpfApplication = System.Windows.Application;
using PDV.Clients.ViewModels.Implementations;
using PDV.Clients.ViewModels.Interfaces;

namespace PDV.Clients
{
    public partial class App : WpfApplication
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IApiClient, ApiClient>();

            services.AddTransient<ILoginViewModel, LoginViewModel>();
            services.AddTransient<IDashboardViewModel, DashboardViewModel>();
            services.AddTransient<ICashViewModel, CashViewModel>();
            services.AddTransient<IProductViewModel, ProductViewModel>();

            services.AddTransient<LoginView>();
            services.AddTransient<Dashboard>();
            services.AddTransient<CashView>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginScreen = _serviceProvider.GetRequiredService<LoginView>();
            var loginStatus = loginScreen.ShowDialog();

            if (loginStatus == true)
            {
                var role = loginScreen.Role;

                FluentWindow mainWindow = role == "Administrador"
                    ? _serviceProvider.GetRequiredService<Dashboard>()
                    : _serviceProvider.GetRequiredService<CashView>();

                Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                this.Shutdown();
            }
        }
    }
}
