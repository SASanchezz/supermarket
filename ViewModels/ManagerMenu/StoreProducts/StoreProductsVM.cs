using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    class StoreProductsVM : ViewModel, IWindowOpeningVM<ManagerStoreProducts>
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
                OnPropertyChanged();
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
    }
}
