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
    class ManagerStoreProductsViewModel: IWindowOpeningViewModel, INotifyPropertyChanged
    {
        private List<string[]> _storeProducts;
        private string[] _selectedStoreProduct;

        private RelayCommand<object> _openAddStoreProductWindowCommand;
        private RelayCommand<object> _openEditStoreProductWindowCommand;
        private RelayCommand<object> _closeCommand;

        public ManagerStoreProductsViewModel()
        {
            UpdateStoreProducts();
        }

        public Action<WindowTypes> OpenWindowViewModel { get; set; }
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
