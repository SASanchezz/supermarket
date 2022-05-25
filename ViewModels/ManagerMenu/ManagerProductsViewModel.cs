using supermarket.Navigation.WindowsNavigation;
using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.ViewModels.ManagerMenu
{
    class ManagerProductsViewModel : IWindowOpeningViewModel, INotifyPropertyChanged
    {
        private List<string[]> _products;
        private string[] _selectedProduct;

        private RelayCommand<object> _openAddProductWindowCommand;
        private RelayCommand<object> _openEditProductWindowCommand;
        private RelayCommand<object> _closeCommand;

        public ManagerProductsViewModel()
        {
            UpdateProducts();
        }

        public Action<WindowTypes> OpenWindowViewModel { get; set; }
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
