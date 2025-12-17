namespace PDV.Clients.ViewModels.Implementations.Customer;

public class CustomerDetailViewModel : Notifier
{
    private string _name = string.Empty;
    private string _email = string.Empty;
    private int _age;
    private string _address = string.Empty;

    public Guid Id { get; set; }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
    }

    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
    }

    public int Age
    {
        get => _age;
        set { _age = value; OnPropertyChanged(); }
    }

    public string Address
    {
        get => _address;
        set { _address = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
    }

    public bool IsNewCustomer => Id == Guid.Empty;

    public bool IsValid =>
        !string.IsNullOrWhiteSpace(Name) &&
        !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(Address) &&
        Age > 0;
}