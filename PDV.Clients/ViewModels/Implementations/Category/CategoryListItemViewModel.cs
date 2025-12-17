namespace PDV.Clients.ViewModels.Implementations.Category;

public class CategoryListItemViewModel : Notifier
{
    private string _name = string.Empty;
    private string _description = string.Empty;
    private bool _isActive = true;

    public int Id { get; set; }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(); }
    }

    public bool IsActive
    {
        get => _isActive;
        set { _isActive = value; OnPropertyChanged(); }
    }

}