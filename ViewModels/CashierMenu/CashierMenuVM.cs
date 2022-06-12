using supermarket.Navigation.ViewModels;
using supermarket.Navigation.WindowViewModels;
using System;
using supermarket.Utils;

namespace supermarket.ViewModels.CashierMenu
{
    /*
     * Controls Cashier Menu View
     */
    internal class CashierMenuVM : NavigatableViewModel<MainViewsTypes>, IWindowOpeningVM<Main>
    {
        public CashierMenuVM()
        {
            OpenProductsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.CashierProducts));
            //
            OpenStoreProductsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.CashierStoreProducts));

        }

        public Action<Main> OpenWindowViewModel { get; set; }
        
        public RelayCommand<object> OpenProductsWindowCommand { get; }
        public RelayCommand<object> OpenStoreProductsWindowCommand { get; }
        public RelayCommand<object> OpenClientsWindowCommand { get; }
        public RelayCommand<object> OpenReceiptsWindowCommand { get; }
    }
}
