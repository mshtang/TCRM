using Caliburn.Micro;
using System.ComponentModel;

namespace TCRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<string> _products;

        public BindingList<string> Products
        {
            get => _products;
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private int _itemQuantity;

        public int ItemQuantity
        {
            get => _itemQuantity;
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public bool CanAddToCart
        {
            get => Products.Count > 0;
        }

        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart()
        {
            return true;
        }

        public void RemoveFromCart()
        {

        }

        private decimal _subtotal;

        public decimal Subtotal
        {
            get => _subtotal;
            set
            {
                _subtotal = value;
                NotifyOfPropertyChange(() => Subtotal);
            }
        }


        private decimal _tax;

        public decimal Tax
        {
            get => _tax;
            set
            {
                _tax = value;
                NotifyOfPropertyChange(() => Tax);
            }
        }


        private decimal _total;

        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                NotifyOfPropertyChange(() => Total);
            }
        }


        public bool CanCheckout
        {
            get => true;
        }

        public void Checkout()
        {

        }
    }
}
