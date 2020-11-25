using Caliburn.Micro;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.ViewModels
{
    public class ProductDisplayModel : Screen
    {
        public int Id;

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal RetailPrice { get; set; }

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

        public TaxCategory Tax { get; set; }
    }
}
