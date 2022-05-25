using supermarket.Navigation.WindowVM;
using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    class StoreProductsVM : IWindowOpeningVM<ManagerStoreProducts>, INotifyPropertyChanged
    {
        private List<string[]> _storeProducts;
        private string[] _selectedStoreProduct;

        private RelayCommand<object> _openAddStoreProductWindowCommand;
        private RelayCommand<object> _openEditStoreProductWindowCommand;
        private RelayCommand<object> _closeCommand;

        public StoreProductsVM()
        {
            UpdateStoreProducts();
        }

        public Action<ManagerStoreProducts> OpenWindowViewModel { get; set; }

        public List<string[]> StoreProducts
        {
            get
            {
                return _storeProducts;
            }
            set
            {
                _storeProducts = value;
                OnPropertyChanged(nameof(StoreProducts));
            }
        }

        public string[] SelectedStoreProduct
        {
            get
            {
                return _selectedStoreProduct;
            }
            set
            {
                _selectedStoreProduct = value;
                OnPropertyChanged();
            }
        }

        public void UpdateStoreProducts()
        {
            StoreProducts = DbQueries.GetAllStoreProducts();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
