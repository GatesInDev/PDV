namespace PDV.Clients.ViewModels.Implementations.Customer;

public class CustomerListItemViewModel : Notifier
{
    private string _name = string.Empty;
    private string _email = string.Empty;
    private int _age;
    private string _address = string.Empty;
    private bool _isActive = true;

    public Guid Id { get; set; }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    public int Age
    {
        get => _age;
        set { _age = value; OnPropertyChanged(); }
    }

    public bool IsActive
    {
        get => _isActive;
        set { _isActive = value; OnPropertyChanged(); }
    }

    public string Address
    {
        get => _address;
        set { _address = value; OnPropertyChanged(); }
    }
}