using System.Windows.Input;

namespace PDV.Clients.ViewModels.Interfaces
{
    public interface IEditorViewModel
    {
        string Title { get; }
        bool IsEditMode { get; } 
        ICommand SaveCommand { get; }
        ICommand CancelCommand { get; }
        ICommand DeleteCommand { get; }
    }
    
}