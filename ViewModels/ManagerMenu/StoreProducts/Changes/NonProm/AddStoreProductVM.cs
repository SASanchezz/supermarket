using supermarket.Data;
using supermarket.Utils;
using System;
using supermarket.Middlewares.StoreProduct;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;
using System.Collections.Generic;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.NonProm
{
    internal class AddStoreProductVM : ViewModel
    {
        private string _upc;
        private string _subProduct = "";
        private string _price;
        private string _amount;

        public AddStoreProductVM()
        {
            AddStoreProductCommand = new RelayCommand<object>(_ => AddStoreProduct(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string Upc
        { 
            get => _upc;
            set 
            {
                _upc = value;
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
                List<string[]> products = Product.GetNotExistingStoreProductsBySubNameOrId(SubProduct);
                
                if (products == null)
                {
                    return new List<string>(0);
                }
                
                List<string> resultProducts = new(products.Count);
                
                foreach (var product in products)
                {
                    resultProducts.Add(product[Product.id_product] + "  --  " + product[Product.product_name]);
                }

                return resultProducts;
            }
        }

        public RelayCommand<object> AddStoreProductCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        private void AddStoreProduct()
        {

            ////Validates entered information
            string result = StoreProductAddValidator.ValidateNotProm(Upc, SubProduct.Split(' ')[0], Price, Amount);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            StoreProduct.AddNonPromStoreProduct(Upc, SubProduct.Split(' ')[0], double.Parse(Price), Amount);

            ResetFields();
            CloseWindow();
        }
        
        private void ResetFields()
        {
            Upc = "";
            SubProduct = "";
            Price = "";
            Amount = "";
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Upc)
                && !string.IsNullOrWhiteSpace(SubProduct)
                && !string.IsNullOrWhiteSpace(Price)
                && !string.IsNullOrWhiteSpace(Amount);
        }
    }
}
