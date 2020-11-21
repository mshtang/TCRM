using Caliburn.Micro;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.ViewModels
{
    public class ProductViewModel : Screen
    {
        public int Id;

        public decimal RetailPrice { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        private int _quantityInStock;

        public int QuantityInStock
        {
            get => _quantityInStock;
            set
            {
                _quantityInStock = value;
                NotifyOfPropertyChange();
            }
        }

        public ProductViewModel()
        {

        }

        public ProductViewModel(ProductModel pm)
        {
            Id = pm.Id;
            RetailPrice = pm.RetailPrice;
            ProductName = pm.ProductName;
            Description = pm.Description;
            QuantityInStock = pm.QuantityInStock;
        }

    }
}
