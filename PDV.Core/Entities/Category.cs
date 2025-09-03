namespace PDV.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Product> Products { get; set; } = new List<Product>(); // 1 : N
    }
}
