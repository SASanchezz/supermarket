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
        private string _id_product;
        private int _category_number;
        private string _product_name;
        private string _manufacturer;
        private string _characteristics;

        private RelayCommand<object> _addProductCommand;
        private RelayCommand<object> _closeCommand;
        public Action Close { get; set; }

        public string IdProduct { get => _id_product; set => _id_product = value; }

        public int CategoryNumber { get => _category_number; set => _category_number = value; }

        public string ProductName { get => _product_name; set => _product_name = value; }

        public string Characteristics { get => _characteristics; set => _characteristics = value; }

        public string Manufacturer { get => _manufacturer; set => _manufacturer = value; }

        public static string[] CategoriesNames { get => Categ.GetAllCategoriesNames(); }

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
            string result = ProductValidator.Validate(_id_product, _product_name, 
                _characteristics, _manufacturer);

            if (result.Length != 0)
            {
                //MessageBox.Show(result);
                return;
            }

            //Query to insert new product
            Product.AddProduct(_id_product, Categ.GetIDByName(CategoriesNames[_category_number])[0], _product_name, 
                _characteristics, _manufacturer);

            Close();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(IdProduct)
                && !string.IsNullOrWhiteSpace(ProductName)
                && !string.IsNullOrWhiteSpace(Characteristics)
                && !string.IsNullOrWhiteSpace(Manufacturer);
        }
    }
}
