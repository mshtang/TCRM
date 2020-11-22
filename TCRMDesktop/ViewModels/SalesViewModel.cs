using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TCRMDesktopUI.Library.Api;

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
            Products = new ObservableCollection<ProductViewModel>(_productList.Select(p => new ProductViewModel(p)));
        }

        private ObservableCollection<ProductViewModel> _products;

        public ObservableCollection<ProductViewModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _itemQuantity;

        public int? ItemQuantity
        {
            get => _itemQuantity;
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanAddToCart));
                NotifyOfPropertyChange(nameof(CanRemoveFromCart));
            }
        }

        private ObservableCollection<ProductViewModel> _cart;

        public ObservableCollection<ProductViewModel> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange();
            }
        }

        private ProductViewModel _selectedItemToAdd;

        public ProductViewModel SelectedItemToAdd
        {
            get => _selectedItemToAdd;
            set
            {
                _selectedItemToAdd = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanAddToCart));
            }
        }

        private ProductViewModel _selectedItemToRemove;

        public ProductViewModel SelectedItemToRemove
        {
            get => _selectedItemToRemove;
            set
            {
                _selectedItemToRemove = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanRemoveFromCart));
            }
        }

        private decimal _subtotal;

        public decimal Subtotal
        {
            get => _subtotal;
            set
            {
                _subtotal = value;
                NotifyOfPropertyChange();
            }
        }

        private decimal _tax;

        public decimal Tax
        {
            get => _tax;
            set
            {
                _tax = value;
                NotifyOfPropertyChange();
            }
        }

        private decimal _total;

        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                NotifyOfPropertyChange();
            }
        }

        public bool CanAddToCart
        {
            get => SelectedItemToAdd?.QuantityInStock >= ItemQuantity && ItemQuantity > 0;
        }

        public void AddToCart()
        {
            if (Cart == null)
                Cart = new ObservableCollection<ProductViewModel>();

            var cartItem = Cart.FirstOrDefault(p => p.Id == SelectedItemToAdd.Id);
            if (cartItem == null)
            {
                Cart.Add(new ProductViewModel
                {
                    Id = SelectedItemToAdd.Id,
                    Description = SelectedItemToAdd.Description,
                    ProductName = SelectedItemToAdd.ProductName,
                    QuantityInStock = ItemQuantity.Value,
                    Tax = SelectedItemToAdd.Tax,
                    RetailPrice = SelectedItemToAdd.RetailPrice
                });
            }
            else
            {
                var itemInCartToUpdate = Cart.FirstOrDefault(p => p.Id == cartItem.Id);
                Cart[Cart.IndexOf(itemInCartToUpdate)].QuantityInStock += ItemQuantity.Value;
            }

            UpdateMoneyToPay();

            var itemToUpdate = Products.FirstOrDefault(p => p.Id == SelectedItemToAdd.Id);
            Products[Products.IndexOf(itemToUpdate)].QuantityInStock -= ItemQuantity.Value;

            ItemQuantity = 1;
        }

        public bool CanRemoveFromCart
        {
            get => SelectedItemToRemove?.QuantityInStock >= ItemQuantity && ItemQuantity > 0;
        }

        public void RemoveFromCart()
        {
            var cartItem = Cart.FirstOrDefault(p => p.Id == SelectedItemToRemove.Id);
            var itemInCartToUpdate = Cart.FirstOrDefault(p => p.Id == cartItem.Id);
            if (itemInCartToUpdate.QuantityInStock - ItemQuantity.Value == 0)
            {
                Cart.Remove(itemInCartToUpdate);
            }
            else
            {
                Cart[Cart.IndexOf(itemInCartToUpdate)].QuantityInStock -= ItemQuantity.Value;
            }


            UpdateMoneyToPay();

            var itemToUpdate = Products.FirstOrDefault(p => p.Id == SelectedItemToAdd.Id);
            Products[Products.IndexOf(itemToUpdate)].QuantityInStock += ItemQuantity.Value;

            ItemQuantity = 1;

        }

        private void UpdateMoneyToPay()
        {
            Subtotal = Math.Round(Cart.Select(p => p.QuantityInStock * p.RetailPrice).Sum(), 2, MidpointRounding.AwayFromZero);
            Tax = Math.Round(Cart.Select(p => p.Tax.TaxRate * p.QuantityInStock * p.RetailPrice).Sum(), 2, MidpointRounding.AwayFromZero);
            Total = Math.Round(Subtotal + Tax, 2, MidpointRounding.AwayFromZero);
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
