using PDV.Clients.ViewModels.Implementations.Category;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface ICategoryViewModel {
        ObservableCollection<CategoryListItemViewModel> Categories { get; }
        CategoryListItemViewModel? SelectedListItem { get; set; }
        CategoryDetailViewModel? SelectedCategory { get; set; }

        bool IsBusy { get; set; }
        string? ErrorMessage { get; set; }

        ICommand LoadCommand { get; }
        ICommand RefreshCommand { get; }
        ICommand NewCategoryCommand { get; }
        ICommand SaveCategoryCommand { get; }
        ICommand DeleteCategoryCommand { get; }
        ICommand BackCommand { get; }

        event Action? RequestClose;
    }
}
