namespace PDV.Clients.ViewModels.Implementations.Category;

public class CategoryDetailViewModel : Notifier
{
    private string _name = string.Empty;
    private string _description = string.Empty;

    public int Id { get; set; }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
    }

    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
    }

    public bool IsValid =>
        !string.IsNullOrWhiteSpace(Name) &&
        !string.IsNullOrWhiteSpace(Description);
}