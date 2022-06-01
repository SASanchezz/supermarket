using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Categ = supermarket.Models.Category;
using Prod = supermarket.Models.Product;
using supermarket.Middlewares.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    internal class EditProductVM : ViewModel
    {
        private string _init_id_product;
        private string _changed_id_product;
        private string _product_name;
        private string _category_number;
        private string _manufacturer;
        private string _characteristics;

        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _closeCommand;

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

        public string CategoryNumber
        {
            get => _category_number;
            set
            {
                _category_number = value;
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

        public static string[] Categories => Categ.GetAllCategoriesNames();

        public RelayCommand<object> UpdateCommand
        {
            get
            {
                return _updateCommand ??= new RelayCommand<object>(_ => UpdateProduct(), CanExecute);
            }
        }
        public RelayCommand<object> DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand<object>(_ => DeleteProduct());
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }
        public Action Close { get; set; }

        public void SetData(string[] data)
        {
            InitIdProduct = data[Prod.id_product];
            ChangedIdProduct = data[Prod.id_product];
            ProductName = data[Prod.product_name];
            CategoryNumber = data[Prod.category_number];
            Characteristics = data[Prod.characteristics];
            Manufacturer = data[Prod.manufacturer];
        }

        private void UpdateProduct()
        {
            string result = ProductValidator.Validate(ChangedIdProduct, ProductName, 
                Characteristics, Manufacturer);

            if (result.Length != 0)
            {
               //MessageBox.Show(result);
                return;
            }

            Prod.UpdateProduct(InitIdProduct, ChangedIdProduct, 
                CategoryNumber, ProductName, Characteristics, Manufacturer);

            Close();
        }

        private void DeleteProduct()
        {
            Prod.DeleteProductByID(InitIdProduct);
            Close();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChangedIdProduct)
                && !string.IsNullOrWhiteSpace(CategoryNumber)
                && !string.IsNullOrWhiteSpace(ProductName)
                && !string.IsNullOrWhiteSpace(Manufacturer)
                && !string.IsNullOrWhiteSpace(Characteristics);
        }
    }
}
