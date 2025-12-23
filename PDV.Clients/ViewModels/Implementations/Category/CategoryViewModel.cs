    using PDV.Application.DTOs.Customer;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Implementations.Customer;
using PDV.Clients.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using PDV.Application.DTOs.Category;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace PDV.Clients.ViewModels.Implementations.Category 
{
    public class CategoryViewModel : Notifier, ICategoryViewModel
    {
        private readonly IApiClient _apiClient;

        private ObservableCollection<CategoryListItemViewModel> _categories = new();
        private CategoryListItemViewModel? _selectedListItem;
        private CategoryDetailViewModel? _selectedCategory;

        private bool _isBusy;
        private string? _errorMessage;
        private bool _hasDashboardAccess;
        private string _backButtonText = "Voltar";

        #region Properties

        public ObservableCollection<CategoryListItemViewModel> Categories
        {
            get => _categories;
            private set { _categories = value; OnPropertyChanged(); }
        }

        public CategoryListItemViewModel? SelectedListItem
        {
            get => _selectedListItem;
            set
            {
                _selectedListItem = value;
                OnPropertyChanged();
                if (value != null)
                {
                    LoadDetailFromList(value);
                }
            }
        }

        public CategoryDetailViewModel? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                RefreshCommandStates();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                RefreshCommandStates();
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool HasDashboardAccess
        {
            get => _hasDashboardAccess;
            set
            {
                _hasDashboardAccess = value;
                OnPropertyChanged();
                UpdateBackButtonText();
            }
        }

        public string BackButtonText
        {
            get => _backButtonText;
            set
            {
                _backButtonText = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand LoadCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand NewCategoryCommand { get; }
        public ICommand SaveCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand BackCommand { get; }

        public event Action? RequestClose;

        #endregion

        public CategoryViewModel(IApiClient apiClient, bool hasDashboardAccess = true)
        {
            _apiClient = apiClient;
            _hasDashboardAccess = hasDashboardAccess;

            LoadCommand = new RelayCommand<object>(OnLoad);
            RefreshCommand = new RelayCommand<object>(OnRefresh, _ => !IsBusy);
            NewCategoryCommand = new RelayCommand<object>(OnNewCategory, _ => !IsBusy);
            SaveCategoryCommand = new RelayCommand<object>(OnSave, CanSave);
            DeleteCategoryCommand = new RelayCommand<object>(OnDelete, CanDelete);
            BackCommand = new RelayCommand<object>(OnBack);

            UpdateBackButtonText();
            OnLoad(null);
        }

        private void UpdateBackButtonText()
        {
            BackButtonText = _hasDashboardAccess ? "Voltar" : "Fechar";
        }

        #region Command Handlers

        private async void OnLoad(object? _)
        {
            await LoadDataAsync();
        }

        private async void OnRefresh(object? _)
        {
            SelectedCategory = null;
            SelectedListItem = null;
            await LoadDataAsync();
        }

        private void OnNewCategory(object? _)
        {
            SelectedListItem = null;

            SelectedCategory = new CategoryDetailViewModel
            {
                Id = 0,
                Name = string.Empty,
                Description = string.Empty
            };
        }

        private void OnBack(object? _)
        {
            RequestClose?.Invoke();
        }

        private void LoadDetailFromList(CategoryListItemViewModel item)
        {
            SelectedCategory = new CategoryDetailViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };
        }

        private async void OnSave(object? _)
        {
            if (SelectedCategory == null) return;

            string name = SelectedCategory.Name?.Trim() ?? string.Empty;
            string description = SelectedCategory.Description?.Trim() ?? string.Empty;

            ErrorMessage = null; 

            if (string.IsNullOrWhiteSpace(name))
            {
                ErrorMessage = "O Nome da categoria é obrigatório.";
                return;
            }

            if (name.Length < 3 || name.Length > 50)
            {
                ErrorMessage = "O Nome deve ter entre 3 e 50 caracteres.";
                return;
            }

            if (!Regex.IsMatch(name, @"^[a-zA-Z\u00C0-\u00FF\s\&]+$"))
            {
                ErrorMessage = "O Nome deve conter apenas letras e espaços (sem números ou símbolos especiais).";
                return;
            }

            if (Regex.IsMatch(name, @"^teste$|^categoria$|^nova$", RegexOptions.IgnoreCase))
            {
                ErrorMessage = "Utilize um nome de categoria válido e específico.";
                return;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                ErrorMessage = "A Descrição é obrigatória.";
                return;
            }

            if (description.Length < 10)
            {
                ErrorMessage = "A Descrição está muito curta. Detalhe melhor o que a categoria engloba.";
                return;
            }

            if (description.Length > 200)
            {
                ErrorMessage = "A Descrição excede o limite de 200 caracteres.";
                return;
            }

            if (!Regex.IsMatch(description, @"^[a-zA-Z0-9\u00C0-\u00FF\s\.,;\-\(\)!?]+$"))
            {
                ErrorMessage = "A Descrição contém caracteres inválidos. Use apenas texto e pontuação básica.";
                return;
            }

            IsBusy = true;

            try
            {
                if (SelectedCategory.Id == 0)
                {
                    var createDto = new CreateCategoryDTO
                    {
                        Name = name,
                        Description = description
                    };

                    await _apiClient.CreateCategoryAsync(createDto);
                }
                else
                {
                    var updateDto = new UpdateCategoryDTO
                    {
                        Name = name,
                        Description = description
                    };

                    await _apiClient.UpdateCategoryAsync(SelectedCategory.Id, updateDto);
                }

                var msgBox = new MessageBox
                {
                    Title = "Sucesso",
                    Content = "Categoria salva com sucesso!",
                    CloseButtonText = "OK"
                };
                await msgBox.ShowDialogAsync();

                ErrorMessage = null;
                SelectedCategory = null;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                var msgBox = new MessageBox
                {
                    Title = "Erro",
                    Content = $"Falha ao salvar: {ex.Message}",
                    CloseButtonText = "OK"
                };
                await msgBox.ShowDialogAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnDelete(object? _)
        {
            if (SelectedCategory == null) return;

            ErrorMessage = null;

            IsBusy = true;
            try
            {
                await _apiClient.DeleteCategoryAsync(SelectedCategory.Id);
                ErrorMessage = null; 
                SelectedCategory = null;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally 
            { 
                IsBusy = false; 
            }
        }

        #endregion

        #region Helper Methods

        private async Task LoadDataAsync()
        {
            IsBusy = true;
            ErrorMessage = null;

            try
            {
                var dtos = await _apiClient.GetAllCategoriesAsync();

                var categories = dtos.Where(c => c.IsActive == true).Select(c => new CategoryListItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).ToList();

                Categories = new ObservableCollection<CategoryListItemViewModel>(categories);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erro de conexão: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanSave(object? _) => !IsBusy && SelectedCategory != null;

        private bool CanDelete(object? _) => !IsBusy && SelectedCategory != null && SelectedCategory.Id != 0;

        private void RefreshCommandStates()
        {
            ((RelayCommand<object>)SaveCategoryCommand).NotifyCanExecuteChanged();
            ((RelayCommand<object>)DeleteCategoryCommand).NotifyCanExecuteChanged();
            ((RelayCommand<object>)NewCategoryCommand).NotifyCanExecuteChanged();
            ((RelayCommand<object>)RefreshCommand).NotifyCanExecuteChanged();
        }

        #endregion
    }
}
