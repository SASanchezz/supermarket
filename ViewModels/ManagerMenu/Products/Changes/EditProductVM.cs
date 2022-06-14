using System;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Categ = supermarket.Models.Category;
using Prod = supermarket.Models.Product;
using supermarket.Middlewares.Product;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    internal class EditProductVM : ViewModel
    {
        private string _init_id_product;
        private string _changed_id_product;
        private string _product_name;
        private string _category_name;
        private string _manufacturer;
        private string _characteristics;

        int ERROR = -1;
        int GOOD = 1;
        string deleteErrorMessage = "Цей продукт використовується";

        public EditProductVM()
        {
            UpdateProductCommand = new RelayCommand<object>(_ => UpdateProduct(), CanExecute);
            DeleteProductCommand = new RelayCommand<object>(_ => DeleteProduct());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string InitIdProduct
        {
            get => _init_id_product;
            set
            {
                _init_id_product = value;
                OnPropertyChanged();
            }
        }

        public string ChangedIdProduct
        {
            get => _changed_id_product;
            set
            {
                _changed_id_product = value;
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

        public string[] SelectiveCategories => Categ.GetAllCategoriesNames();

        public RelayCommand<object> UpdateProductCommand { get; }
        
        public RelayCommand<object> DeleteProductCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        public void SetData(string[] data)
        {
            InitIdProduct = data[Prod.id_product];
            ChangedIdProduct = data[Prod.id_product];
            ProductName = data[Prod.product_name];
            CategoryName = data[Prod.category_name];
            Characteristics = data[Prod.characteristics];
            Manufacturer = data[Prod.manufacturer];
        }

        public void UpdateSelectiveCategories()
        {
            OnPropertyChanged(nameof(SelectiveCategories));
        }

        private void UpdateProduct()
        {
            try
            {
                ProductValidator.ValidateUpdate(InitIdProduct, ChangedIdProduct, ProductName, Characteristics, Manufacturer);
                
                Prod.UpdateProduct(InitIdProduct, ChangedIdProduct, 
                    Categ.GetIDByName(CategoryName)[0], ProductName, Characteristics, Manufacturer);

                CloseWindow();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DeleteProduct()
        {
            int response = Prod.DeleteProductByID(InitIdProduct);
            if (response == ERROR) MessageBox.Show(deleteErrorMessage);
            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChangedIdProduct)
                && !string.IsNullOrWhiteSpace(ProductName)
                && !string.IsNullOrWhiteSpace(Manufacturer)
                && !string.IsNullOrWhiteSpace(Characteristics);
        }

    }
}
