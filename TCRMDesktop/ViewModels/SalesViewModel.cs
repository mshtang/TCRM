using Caliburn.Micro;
using System.ComponentModel;
using TCRMDesktopUI.Library.Api;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndpoint _productEndpoint;

        public SalesViewModel(IProductEndpoint productEndPoint)
        {
            _productEndpoint = productEndPoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            var _productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(_productList);
        }

        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
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
            get => true;
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
