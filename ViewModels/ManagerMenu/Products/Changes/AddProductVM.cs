using supermarket.Data;
using supermarket.Utils;
using System;
using Categ = supermarket.Models.Category;
using supermarket.Middlewares.SignUp;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;
using supermarket.Middlewares.Product;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    internal class AddProductVM : ViewModel
    {
        private RelayCommand<object> _addProductCommand;
        private RelayCommand<object> _closeCommand;
        public Action Close { get; set; }

        public string IdProduct { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public string Characteristics { get; set; }

        public string Manufacturer { get; set; }

        public static string[] CategoriesNames => Categ.GetAllCategoriesNames();

        public RelayCommand<object> AddProductCommand
        {
            get
            {
                return _addProductCommand ??= new RelayCommand<object>(_ => AddProduct(), CanExecute);
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }
        private void AddProduct()
        {
            ////Validates entered information
            string result = ProductValidator.Validate(IdProduct, ProductName, 
                Characteristics, Manufacturer);

            if (result.Length != 0)
            {
                //MessageBox.Show(result);
                return;
            }

            //Query to insert new product
            Product.AddProduct(IdProduct, Categ.GetIDByName(CategoryName)[0], ProductName, 
                Characteristics, Manufacturer);

            Close();
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
