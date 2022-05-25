using supermarket.Navigation;
using supermarket.Navigation.ViewsNavigation;
using supermarket.Navigation.WindowsNavigation;
using supermarket.ViewModels.CashierMenu;
using supermarket.ViewModels.ManagerMenu;
using supermarket.ViewModels.ManagerMenu.Categories;
using supermarket.ViewModels.ManagerMenu.Customers;
using supermarket.ViewModels.ManagerMenu.Employees;
using supermarket.ViewModels.ManagerMenu.Products;
using supermarket.ViewModels.ManagerMenu.StoreProducts;
using supermarket.ViewModels.SignIn;
using System;

namespace supermarket.ViewModels
{
    /* 
     * Controls Main Window (Starting Window)
     * Set navigating of Views as Sign In View, Manager Menu View, Cashier Menu View
     *  and Windows as (Manager Menu) Manager Employees Window, ..., ...
     */
    internal class MainWindowVM : WindowVMAndVMNavigator
    {
        // controllable ViewModels
        private SignInVM _signInVM;
            // windows opening ViewModels
        private ManagerMenuVM _managerMenuVM;
        private CashierMenuVM _cashierMenuVM;
            //
        //
        public MainWindowVM() 
        {
            IsEnabled = true;

            // views navigating setup
            _signInVM = new SignInVM(() => Navigate(ViewTypes.ManagerMenu), () => Navigate(ViewTypes.CashierMenu));
            _managerMenuVM = new ManagerMenuVM(() => Navigate(ViewTypes.SignIn)); 
            _cashierMenuVM = new CashierMenuVM(() => Navigate(ViewTypes.SignIn)); 
            //

            Navigate(ViewTypes.SignIn); // set SignIn View

            SetWindowOpeningViewModel(new IWindowOpeningVM[] { _managerMenuVM, _cashierMenuVM });
        }

        protected override INavigatableWindowVM CreateWindowViewModel(WindowTypes type)
        {
            INavigatableWindowVM _windowViewModel;
            switch (type)
            {
                case WindowTypes.ManagerEmployees:
                    _windowViewModel = new EmployeesWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case WindowTypes.ManagerCustomers:
                    _windowViewModel = new CustomersWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case WindowTypes.ManagerCategories:
                    _windowViewModel = new CategoriesWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case WindowTypes.ManagerProducts:
                    _windowViewModel = new ProductsWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case WindowTypes.ManagerStoreProducts:
                    _windowViewModel = new StoreProductsWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                default:
                    return null;
            }
        }

        protected override INavigatableVM CreateViewModel(ViewTypes type)
        {
            switch (type)
            {
                case ViewTypes.SignIn:
                    return _signInVM;
                case ViewTypes.ManagerMenu:
                    return _managerMenuVM;
                case ViewTypes.CashierMenu:
                    return _cashierMenuVM;
                default:
                    return null;
            }
        }
    }
}
