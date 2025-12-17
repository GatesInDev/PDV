using PDV.Application.DTOs.Customer;
using PDV.Clients.Services.Interfaces;
using PDV.Clients.ViewModels.Implementations.Customer;
using PDV.Clients.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

        public CategoryViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            LoadCommand = new RelayCommand<object>(OnLoad);
            RefreshCommand = new RelayCommand<object>(OnRefresh, _ => !IsBusy);
            NewCategoryCommand = new RelayCommand<object>(OnNewCategory, _ => !IsBusy);
            SaveCategoryCommand = new RelayCommand<object>(OnSave, CanSave);
            DeleteCategoryCommand = new RelayCommand<object>(OnDelete, CanDelete);
            BackCommand = new RelayCommand<object>(OnBack);

            OnLoad(null);
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

            if (!SelectedCategory.IsValid)
            {
                ErrorMessage = "Preencha todos os campos obrigatórios (*).";
                return;
            }

            IsBusy = true;
            ErrorMessage = null;

            try
            {
                if (SelectedCategory.Id == 0)
                {
                    var createDto = new CreateCategoryDTO()
                    {
                        Name = SelectedCategory.Name,
                        Description = SelectedCategory.Description
                    };

                    await _apiClient.CreateCategoryAsync(createDto);
                }
                else
                {
                    var updateDto = new UpdateCategoryDTO()
                    {
                        Name = SelectedCategory.Name,
                        Description = SelectedCategory.Description
                    };

                    await _apiClient.UpdateCategoryAsync(SelectedCategory.Id, updateDto);
                }

                var msgBox = new MessageBox { Title = "Sucesso", Content = "Salvo com sucesso!", CloseButtonText = "OK" };
                await msgBox.ShowDialogAsync();

                SelectedCategory = null;
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                var msgBox = new MessageBox { Title = "Erro", Content = ex.Message, CloseButtonText = "OK" };
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

            var confirmBox = new MessageBox
            {
                Title = "Confirmação",
                Content = $"Excluir '{SelectedCategory.Name}'?",
                PrimaryButtonText = "Sim",
                CloseButtonText = "Não"
            };

            if (await confirmBox.ShowDialogAsync() == MessageBoxResult.Primary)
            {
                IsBusy = true;
                try
                {
                    await _apiClient.DeleteCategoryAsync(SelectedCategory.Id);
                    SelectedCategory = null;
                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    var msgBox = new MessageBox { Title = "Erro", Content = ex.Message, CloseButtonText = "OK" };
                    await msgBox.ShowDialogAsync();
                }
                finally { IsBusy = false; }
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
