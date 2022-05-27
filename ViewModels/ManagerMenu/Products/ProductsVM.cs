using supermarket.Navigation.WindowVM;
using supermarket.Utils;
using System;
using System.Collections.Generic;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    class ProductsVM : ViewModel, IWindowOpeningVM<ManagerProducts>
    {
        private List<string[]> _products;
        private string[] _selectedProduct;

        private RelayCommand<object> _openAddProductWindowCommand;
        private RelayCommand<object> _openEditProductWindowCommand;
        private RelayCommand<object> _closeCommand;

        public ProductsVM()
        {
            UpdateProducts();
        }

        public Action<ManagerProducts> OpenWindowViewModel { get; set; }

        public List<string[]> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                OnPropertyChanged();
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

        public void UpdateProducts()
        {
            Products = DbQueries.GetAllProducts();
        }
    }
}
