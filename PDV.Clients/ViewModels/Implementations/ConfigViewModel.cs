using Microsoft.Win32;
using PDV.Clients.Models;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Interfaces;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels.Implementations
{
    public class ConfigViewModel : Notifier, IConfigViewModel
    {
        private readonly IApiClient _repository;
        private byte[] _currentLogoBytes;

        private string _storeName;
        public string StoreName
        {
            get => _storeName;
            set { _storeName = value; OnPropertyChanged(); }
        }

        private string _cnpj;
        public string Cnpj
        {
            get => _cnpj;
            set { _cnpj = value; OnPropertyChanged(); }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }

        private string _printerName;
        public string PrinterName
        {
            get => _printerName;
            set { _printerName = value; OnPropertyChanged(); }
        }

        private ImageSource _logoDisplay;
        public ImageSource LogoDisplay
        {
            get => _logoDisplay;
            set { _logoDisplay = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand SelectLogoCommand { get; }
        public ICommand SaveConfigCommand { get; }
        public ICommand BackCommand { get; }

        public event Action? RequestClose;

        public ConfigViewModel(IApiClient repository)
        {
            _repository = repository;

            BackCommand = new RelayCommand<object>(OnBack);
            SelectLogoCommand = new RelayCommand<object>(ExecuteSelectLogo);
            SaveConfigCommand = new RelayCommand<object>(async (object? _) => await ExecuteSaveConfig());

            LoadCurrentData();
        }

        private async void LoadCurrentData()
        {
            var config = await _repository.GetConfigAsync();
            if (config != null)
            {
                StoreName = config.NomeFantasia;
                Cnpj = config.Cnpj;
                Address = config.Endereco;
                PrinterName = config.Impressora;

                if (config.Logo != null && config.Logo.Length > 0)
                {
                    _currentLogoBytes = config.Logo;
                    UpdateLogoDisplay(_currentLogoBytes);
                }
            }
        }

        private void ExecuteSelectLogo(object? _)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Imagens|*.png;*.jpg;*.jpeg;*.bmp",
                Title = "Selecione o Logotipo da Loja"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);

                    if (fileBytes.Length > 2 * 1024 * 1024)
                    {
                        ErrorMessage = "Sua imagem e muito grande, tente uma imagem de no maximo 2 mb.";
                        return;
                    }

                    _currentLogoBytes = fileBytes;

                    UpdateLogoDisplay(fileBytes);
                }
                catch (Exception ex)
                {
                    ErrorMessage = "Erro ao carregar a imagem.";
                }
            }
        }

        private void UpdateLogoDisplay(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(bytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            LogoDisplay = image;
        }

        private async Task ExecuteSaveConfig()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(StoreName))
                {
                    ErrorMessage = "O Nome Fantasia é obrigatório.";
                    return;
                }

                var configToSave = new ConfigModel
                {
                    NomeFantasia = StoreName,
                    Cnpj = Cnpj,
                    Endereco = Address,
                    Impressora = PrinterName,

                    Logo = _currentLogoBytes
                };

                await _repository.SaveConfigAsync(configToSave);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erro ao salvar configurações: {ex.Message}";
            }
        }

        private void OnBack(object? _)
        {
            RequestClose?.Invoke();
        }
    }
}
