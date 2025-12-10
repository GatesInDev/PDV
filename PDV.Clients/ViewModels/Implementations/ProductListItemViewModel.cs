    using PDV.Clients.ViewModels;

namespace PDV.Clients.ViewModels.Implementations
{
    /// <summary>
    /// ViewModel para exibição de item na lista Master de produtos.
    /// </summary>
    public class ProductListItemViewModel
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string MetricUnit { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}