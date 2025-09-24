namespace PDV.Core.Entities
{
    public class Sales
    {
        public Guid Id { get; set; }
        public List<Product> Products = new List<Product>();
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public string PaymentMethod { get; set; }
        public string CashOperator { get; set; }

    }
}
