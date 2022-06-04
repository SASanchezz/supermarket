using supermarket.Utils;
using System;
using supermarket.Navigation.ViewModels;
using supermarket.Navigation.WindowViewModels;

namespace supermarket.ViewModels.ManagerMenu
{
    /*
     * Controls Manager Menu View
     */
    internal class ManagerMenuVM : NavigatableViewModel, IWindowOpeningVM<Main>
    {
        public ManagerMenuVM()
        {
            OpenEmployeesWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerEmployees));
            OpenProductsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerProducts));
            OpenCategoriesWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerCategories));
            OpenClientsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerCustomers));
            OpenStoreProductsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerStoreProducts));
            OpenReceiptsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerReceipts));
            GoToSignInCommand = new RelayCommand<object>(_ => ChangeViewModel(VMNavigationTypes.SignIn));
        }

        public Action<Main> OpenWindowViewModel { get; set; }

        public RelayCommand<object> OpenEmployeesWindowCommand { get; }

        public RelayCommand<object> OpenProductsWindowCommand { get; }

        public RelayCommand<object> OpenCategoriesWindowCommand { get; }

        public RelayCommand<object> OpenClientsWindowCommand { get; }

        public RelayCommand<object> OpenStoreProductsWindowCommand { get; }
        
        public RelayCommand<object> OpenReceiptsWindowCommand { get; }

        public RelayCommand<object> GoToSignInCommand { get; }
    }
}
