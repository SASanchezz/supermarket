using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    class ProductsVM : ViewModel, IWindowOpeningVM<ManagerProducts>, INotifyPropertyChanged
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

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }

        public void UpdateProducts()
        {
            Products = DbQueries.GetAllProducts();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
