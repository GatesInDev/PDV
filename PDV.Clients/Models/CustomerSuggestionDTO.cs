namespace PDV.Clients.Models
{
    public class CustomerSuggestionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public override string ToString() => Name;
    }
}
