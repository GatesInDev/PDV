namespace PDV.Core.Entities
{
    public class Stock
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; } // FK

        public Product Product { get; set; } // Navigation Property

        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
