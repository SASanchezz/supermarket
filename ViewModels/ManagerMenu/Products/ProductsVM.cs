using supermarket.Navigation.WindowViewModels;
using supermarket.Printing;
using supermarket.Utils;
using Categ = supermarket.Models.Category;
using Prod = supermarket.Models.Product;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    class ProductsVM : ViewModel, IWindowOpeningVM<ManagerProducts>
    {
        private const string AllString = "Всі";

        private List<string[]> _products;
        private string[] _selectedProduct;

        private string _filteredName = "";

        private string[] _selectiveCategories;
        private string _selectedCategory = AllString;

        private RelayCommand<object> _openAddProductWindowCommand;
        private RelayCommand<object> _openEditProductWindowCommand;
        private RelayCommand<object> _printCommand;
        private RelayCommand<object> _closeCommand;

        public ProductsVM()
        {
            UpdateProducts();
            SetSelectiveCategories();
        }

        public Action<ManagerProducts> OpenWindowViewModel { get; set; }

        public Action Close { get; set; }

        public List<string[]> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public string[] SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public string FilteredName
        {
            get
            {
                return _filteredName;
            }
            set
            {
                _filteredName = value;

                UpdateProducts();
                OnPropertyChanged();
            }
        }

        public string[] SelectiveCategories => _selectiveCategories;

        public string SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                if (value == null)
                {
                    _selectedCategory = AllString;
                }
                else
                {
                    _selectedCategory = value;
                }
                OnPropertyChanged();
                UpdateProducts();
            }
        }

        public RelayCommand<object> OpenAddProductWindowCommand
        {
            get
            {
                return _openAddProductWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerProducts.AddProduct));
            }
        }

        public RelayCommand<object> OpenEditEmployeeWindowCommand
        {
            get
            {
                return _openEditProductWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerProducts.EditProduct));
            }
        }

        public RelayCommand<object> PrintCommand
        {
            get
            {
                return _printCommand ??= new RelayCommand<object>(_ =>
                {
                    Printer.PrintDataGrid(Products, new string[]
                    {
                        "Колонка1",
                        "Колонка2",
                        "Колонка3",
                        "Колонка4",
                        "Колонка5",
                    });
                });
            }
        }


        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }

        public void UpdateProducts()
        {
            Products = Prod.GetAllProducts(_selectedCategory, _filteredName);
        }

        private void SetSelectiveCategories() {

            _selectiveCategories = new string[Categ.GetAllCategories().Count + 1];
            _selectiveCategories.SetValue(AllString, 0);
            for (int i = 0; i < Categ.GetAllCategories().Count; ++i)
            {
                _selectiveCategories[i + 1] = Categ.GetAllCategoriesNames()[i];
            }
        }
    }
}

