using Caliburn.Micro;
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
                NotifyOfPropertyChange(nameof(Products));
            }
        }

        private int? _itemQuantity;

        public int? ItemQuantity
        {
            get => _itemQuantity;
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private ObservableCollection<ProductViewModel> _cart;

        public ObservableCollection<ProductViewModel> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange(nameof(Cart));
            }
        }

        private ProductViewModel _selectedItemToAdd;

        public ProductViewModel SelectedItemToAdd
        {
            get => _selectedItemToAdd;
            set
            {
                _selectedItemToAdd = value;
                NotifyOfPropertyChange(() => SelectedItemToAdd);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private ProductViewModel _selectedItemToRemove;

        public ProductViewModel SelectedItemToRemove
        {
            get => _selectedItemToRemove;
            set
            {
                _selectedItemToRemove = value;
                NotifyOfPropertyChange(() => SelectedItemToRemove);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
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
                    RetailPrice = SelectedItemToAdd.RetailPrice
                });
            }
            else
            {
                var itemInCartToUpdate = Cart.FirstOrDefault(p => p.Id == cartItem.Id);
                Cart[Cart.IndexOf(itemInCartToUpdate)].QuantityInStock += ItemQuantity.Value;
            }

            Subtotal = Cart.Select(p => p.QuantityInStock * p.RetailPrice).Sum();

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


            Subtotal = Cart.Select(p => p.QuantityInStock * p.RetailPrice).Sum();

            var itemToUpdate = Products.FirstOrDefault(p => p.Id == SelectedItemToAdd.Id);
            Products[Products.IndexOf(itemToUpdate)].QuantityInStock += ItemQuantity.Value;

            ItemQuantity = 1;

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
