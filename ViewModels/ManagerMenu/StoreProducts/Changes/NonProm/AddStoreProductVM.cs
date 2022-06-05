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
        private string _UPC;
        private string _subProduct = "";
        private string _price;
        private string _amount;

        private RelayCommand<object> _addStoreProductCommand;
        private RelayCommand<object> _closeCommand;

        public Action Close { get; set; }

        public string UPC
        { 
            get => _UPC;
            set {
            _UPC = value;
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
            get {
                List<string[]> products = Product.GetProductBySubNameOrId(SubProduct);
                if (products == null) return new List<string>(0);
                List<string> resultProducts = new(products.Count);
                for (int i = 0; i < products.Count; i++)
                {
                    resultProducts.Add(products[i][Product.id_product] + "  --  " + products[i][Product.product_name]);
                }

                return resultProducts;
            }
            set { }
        }

        public RelayCommand<object> AddStoreProductCommand
        {
            get => _addStoreProductCommand ??= new RelayCommand<object>(_ => AddStoreProduct(), CanExecute);
        }

        public RelayCommand<object> CloseCommand
        {
            get => _closeCommand ??= new RelayCommand<object>(_ => Close());
        }

        private void AddStoreProduct()
        {

            ////Validates entered information
            string result = StoreProductAddValidator.ValidateNotProm(UPC, SubProduct.Split(' ')[0], Price, Amount);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            StoreProduct.AddNonPromStoreProduct(UPC, SubProduct.Split(' ')[0], double.Parse(Price), Amount);

            UPC = "";
            SubProduct = "";
            Price = "";
            Amount = "";

            Close();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(UPC)
                && !string.IsNullOrWhiteSpace(SubProduct)
                && !string.IsNullOrWhiteSpace(Price)
                && !string.IsNullOrWhiteSpace(Amount);
        }
    }
}
