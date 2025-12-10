using PDV.Clients; 

namespace PDV.Clients.Models
{
    public class ProductModel : Notifier
    {
        private int _id;
        private string _sku;
        private string _name;
        private decimal _price;
        private int _quantity;
        private string _metricUnit;
        private int _categoryId;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Sku
        {
            get => _sku;
            set { _sku = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public string MetricUnit
        {
            get => _metricUnit;
            set { _metricUnit = value; OnPropertyChanged(); }
        }

        public int CategoryId
        {
            get => _categoryId;
            set { _categoryId = value; OnPropertyChanged(); }
        }
    }
}