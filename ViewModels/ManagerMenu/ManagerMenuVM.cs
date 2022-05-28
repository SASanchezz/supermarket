using supermarket.Utils;
using System;
using supermarket.Navigation.ViewModels;
using supermarket.Navigation.WindowViewModels;

namespace supermarket.ViewModels.ManagerMenu
{
    /*
     * Controls Manager Menu View
     */
    internal class ManagerMenuVM : IWindowOpeningVM<Main>, INavigatableVM
    {
        private RelayCommand<object> _openEmployeesWindowCommand; // +
        private RelayCommand<object> _openProductsWindowCommand;
        private RelayCommand<object> _openCategoriesWindowCommand;
        private RelayCommand<object> _openClientsWindowCommand;
        private RelayCommand<object> _openStoreProductsWindowCommand;

        private RelayCommand<object> _goToSignInCommand; // +

        private Action _goToSignIn;

        public ManagerMenuVM(Action goToSignIn)
        {
            _goToSignIn = goToSignIn;
        }

        public Action<Main> OpenWindowViewModel { get; set; }

        public VMNavigationTypes ViewType => VMNavigationTypes.ManagerMenu;

        public RelayCommand<object> OpenEmployeesWindowCommand
        {
            get
            {
                return _openEmployeesWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerEmployees));
            }
        }

        public RelayCommand<object> OpenProductsWindowCommand
        {
            get
            {
                return _openProductsWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerProducts));
            }
        }

        public RelayCommand<object> OpenCategoriesWindowCommand
        {
            get
            {
                return _openCategoriesWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerCategories));
            }
        }

        public RelayCommand<object> OpenClientsWindowCommand
        {
            get
            {
                return _openClientsWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerCustomers));
            }
        }

        public RelayCommand<object> OpenStoreProductsWindowCommand
        {
            get
            {
                return _openStoreProductsWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(Main.ManagerStoreProducts));
            }
        }

        public RelayCommand<object> GoToSignInCommand
        {
            get
            {
                return _goToSignInCommand ??= new RelayCommand<object>(_ => _goToSignIn());
            }
        }
    }
}
