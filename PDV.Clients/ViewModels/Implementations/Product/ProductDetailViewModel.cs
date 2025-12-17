using PDV.Application.DTOs.Category;
using System.Collections.ObjectModel;

namespace PDV.Clients.ViewModels.Implementations.Product
{
    /// <summary>
    /// ViewModel para edição detalhada de um produto (Detail no Master-Detail).
    /// </summary>
    public class ProductDetailViewModel : Notifier
    {
        private string _sku = string.Empty;
        private string _name = string.Empty;
        private int _categoryId;
        private decimal _price;
        private int _stockQuantity;
        private string _metricUnit = "Un";

        public Guid Id { get; set; }

        public string Sku
        {
            get => _sku;
            set { _sku = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
        }

        public int CategoryId
        {
            get => _categoryId;
            set { _categoryId = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            set { _stockQuantity = value; OnPropertyChanged(); }
        }

        public string MetricUnit
        {
            get => _metricUnit;
            set { _metricUnit = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsValid)); }
        }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Helpers
        public bool IsNewProduct => Id == Guid.Empty;

        public bool IsValid =>
            !string.IsNullOrWhiteSpace(Sku) &&
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(MetricUnit) &&
            Price > 0 &&
            CategoryId > 0;
    }
}