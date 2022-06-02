using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    internal class StoreProductsVM : ViewModel, IWindowOpeningVM<ManagerStoreProducts>
    {
        private List<string[]> _storeProducts;
        private string[] _selectedStoreProduct;

        public StoreProductsVM()
        {
            UpdateStoreProducts();
        }

        public Action<ManagerStoreProducts> OpenWindowViewModel { get; set; }

        public List<string[]> StoreProducts
        {
            get => _storeProducts;
            set
            {
                _storeProducts = value;
                OnPropertyChanged();
            }
        }

        public string[] SelectedStoreProduct
        {
            get => _selectedStoreProduct;
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
