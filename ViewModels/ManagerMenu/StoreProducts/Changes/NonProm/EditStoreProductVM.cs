using supermarket.Utils;
using System;
using supermarket.Middlewares.StoreProduct;
using System.Windows;
using supermarket.Models;
using System.Collections.Generic;
using supermarket.Navigation.ViewModels;
using supermarket.ViewModels.ManagerMenu.Categories.Changes;
using StrProduct = supermarket.Models.StoreProduct;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.NonProm
{
    /*
     * Controls ManagerEditEmployee View
     */
    internal class EditStoreProductVM : NavigatableViewModel<EditStoreProductViewsTypes>
    {
        private string _initUpc;
        private string _changedUpc;
        private string _subProduct;
        private string _price;
        private string _amount;

        int ERROR = -1;
        int GOOD = 1;
        string deleteErrorMessage = "Цей чек використовується";

        public EditStoreProductVM()
        {
            UpdateCommand = new RelayCommand<object>(_ => UpdateStoreProduct(), CanExecute);
            DeleteCommand = new RelayCommand<object>(_ => DeleteStoreProduct());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string ChangedUpc
        {
            get => _changedUpc;
            set
            {
                _changedUpc = value;
                OnPropertyChanged();
            }
        }

        public string SubProduct
        {
            get => _subProduct;
            set
            {
                _subProduct = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectiveProducts));
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public string Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public List<string> SelectiveProducts
        {
            get
            {
                List<string[]> products = Product.GetProductBySubNameOrId(SubProduct);
                if (products == null) return new List<string>(0);
                List<string> resultProducts = new(products.Count);
                for (int i = 0; i < products.Count; i++)
                {
                    resultProducts.Add(products[i][Product.id_product] + "  --  " + products[i][Product.product_name]);
                }

                return resultProducts;
            }
        }

        public RelayCommand<object> UpdateCommand { get; }
        
        public RelayCommand<object> DeleteCommand { get; }
        
        public RelayCommand<object> CloseCommand { get; }

        public override void SetData(string[] data)
        {
            string[] productInfo = Product.GetProductByID(data[StrProduct.id_product]);

            _initUpc = data[StrProduct.UPC];
            ChangedUpc = data[StrProduct.UPC];
            SubProduct = productInfo[Product.id_product] + "  --  " + productInfo[Product.product_name];
            Price = data[StrProduct.selling_price];
            Amount = data[StrProduct.products_number];
        }

        private void UpdateStoreProduct()
        {
            string result = StoreProductEditValidator.ValidateNotProm(_initUpc, _changedUpc, _subProduct.Split(' ')[0], _price, _amount);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            StrProduct.UpdateNonPromStoreProduct(_initUpc, _changedUpc, _subProduct.Split(' ')[0], double.Parse(_price), _amount);

            CloseWindow();
        }

        private void DeleteStoreProduct()
        {
            int response = StrProduct.DeleteStoreProductByUPC(_initUpc);
            if (response == ERROR) MessageBox.Show(deleteErrorMessage);
            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChangedUpc)
                && !string.IsNullOrWhiteSpace(SubProduct)
                && !string.IsNullOrWhiteSpace(Price)
                && !string.IsNullOrWhiteSpace(Amount);
        }
    }
}
