using supermarket.Data;
using supermarket.Utils;
using System;
using System.Windows;
using Categ = supermarket.Models.Category;
using supermarket.Middlewares.SignUp;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;
using supermarket.Middlewares.Product;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    internal class AddProductVM : ViewModel
    {
        private string _id_product;
        private string _product_name;
        private string _category_name;
        private string _manufacturer;
        private string _characteristics;
        
        public AddProductVM()
        {
            AddProductCommand = new RelayCommand<object>(_ => AddProduct(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string IdProduct
        {
            get => _id_product;
            set
            {
                _id_product = value;
                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get => _product_name;
            set
            {
                _product_name = value;
                OnPropertyChanged();
            }
        }

        public string CategoryName
        {
            get => _category_name;
            set
            {
                _category_name = value;
                OnPropertyChanged();
            }
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnPropertyChanged();
            }
        }

        public string Characteristics
        {
            get => _characteristics;
            set
            {
                _characteristics = value;
                OnPropertyChanged();
            }
        }

        public static string[] CategoriesNames => Categ.GetAllCategoriesNames();

        public RelayCommand<object> AddProductCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        public void Reset()
        {
            IdProduct = null;
            ProductName = null;
            CategoryName = null;
            Characteristics = null;
            Manufacturer = null;
        }
        
        private void AddProduct()
        {
            ////Validates entered information
            string result = ProductValidator.Validate(IdProduct, ProductName, 
                Characteristics, Manufacturer);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new product
            Product.AddProduct(IdProduct, Categ.GetIDByName(CategoryName)[0], ProductName, 
                Characteristics, Manufacturer);

            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(IdProduct)
                && !string.IsNullOrWhiteSpace(ProductName)
                && !string.IsNullOrWhiteSpace(Characteristics)
                && !string.IsNullOrWhiteSpace(CategoryName)
                && !string.IsNullOrWhiteSpace(Manufacturer);
        }
    }
}
