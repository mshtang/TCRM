using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TCRMDesktopUI.Library.Api;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndpoint _productEndpoint;
        private ISaleEndpoint _saleEndpoint;
        private IMapper _mapper;

        public SalesViewModel(IProductEndpoint productEndPoint, ISaleEndpoint saleEndpoint, IMapper mapper)
        {
            _productEndpoint = productEndPoint;
            _saleEndpoint = saleEndpoint;
            _mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            var _productList = await _productEndpoint.GetAll();
            var products = _mapper.Map<List<ProductDisplayModel>>(_productList);
            Products = new ObservableCollection<ProductDisplayModel>(products);
        }

        private ObservableCollection<ProductDisplayModel> _products;

        public ObservableCollection<ProductDisplayModel> Products
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

        private ObservableCollection<ProductDisplayModel> _cart;

        public ObservableCollection<ProductDisplayModel> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange();
            }
        }

        private ProductDisplayModel _selectedItemToAdd;

        public ProductDisplayModel SelectedItemToAdd
        {
            get => _selectedItemToAdd;
            set
            {
                _selectedItemToAdd = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanAddToCart));
            }
        }

        private ProductDisplayModel _selectedItemToRemove;

        public ProductDisplayModel SelectedItemToRemove
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
                Cart = new ObservableCollection<ProductDisplayModel>();

            var cartItem = Cart.FirstOrDefault(p => p.Id == SelectedItemToAdd.Id);
            if (cartItem == null)
            {
                Cart.Add(new ProductDisplayModel
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
            NotifyOfPropertyChange(nameof(CanCheckout));
        }

        public bool CanCheckout => Cart?.Count > 0;

        public async Task Checkout()
        {
            var sale = new Sale();
            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetail
                {
                    Product = item.Id,
                    Quantity = item.QuantityInStock
                });
            }

            await _saleEndpoint.PostSale(sale);
            await ResetSalesViewModel();
        }

        private async Task ResetSalesViewModel()
        {
            Cart = new ObservableCollection<ProductDisplayModel>();
            ItemQuantity = null;
            Subtotal = 0;
            Tax = 0;
            Total = 0;
            var _productList = await _productEndpoint.GetAll();
            var products = _mapper.Map<List<ProductDisplayModel>>(_productList);
            Products = new ObservableCollection<ProductDisplayModel>(products);
        }
    }
}
