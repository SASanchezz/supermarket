using supermarket.Navigation.WindowViewModels;
using supermarket.Printing;
using supermarket.Utils;
using Categ = supermarket.Models.Category;
using Prod = supermarket.Models.Product;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    internal class ProductsVM : ViewModel, IWindowOpeningVM<ManagerProducts>
    {
        private const string AllString = "Всі";

        private List<string[]> _products;
        private string[] _selectedProduct;

        private string _filteredName = "";
        private string _selectedCategory = AllString;

        public ProductsVM()
        {
            OpenAddProductWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(ManagerProducts.AddProduct));
            OpenEditProductWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(ManagerProducts.EditProduct));
            PrintProductsCommand = new RelayCommand<object>(_ => PrintProducts());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
            SetSelectiveCategories();
        }

        public Action<ManagerProducts> OpenWindowViewModel { get; set; }
        
        public List<string[]> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public string[] SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public string FilteredName
        {
            get => _filteredName;
            set
            {
                _filteredName = value;

                UpdateProducts();
                OnPropertyChanged();
            }
        }

        public string[] SelectiveCategories { get; private set; }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value ?? AllString;
                OnPropertyChanged();
                UpdateProducts();
            }
        }

        public RelayCommand<object> OpenAddProductWindowCommand { get; }

        public RelayCommand<object> OpenEditProductWindowCommand { get; }

        public RelayCommand<object> PrintProductsCommand { get; }
        
        public RelayCommand<object> CloseCommand { get; }

        public void UpdateProducts()
        {
            Products = Prod.GetAllProducts(_selectedCategory, _filteredName);
        }

        public void Reset()
        {
            SelectedCategory = null;
            FilteredName = null;
        }

        private void SetSelectiveCategories() {

            SelectiveCategories = new string[Categ.GetAllCategories().Count + 1];
            SelectiveCategories.SetValue(AllString, 0);
            for (int i = 0; i < Categ.GetAllCategories().Count; ++i)
            {
                SelectiveCategories[i + 1] = Categ.GetAllCategoriesNames()[i];
            }
        }

        private void PrintProducts()
        {
            
        }
    }
}

