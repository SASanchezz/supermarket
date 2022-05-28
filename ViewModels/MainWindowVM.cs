using supermarket.ViewModels.CashierMenu;
using supermarket.ViewModels.ManagerMenu;
using supermarket.ViewModels.ManagerMenu.Categories;
using supermarket.ViewModels.ManagerMenu.Customers;
using supermarket.ViewModels.ManagerMenu.Employees;
using supermarket.ViewModels.ManagerMenu.Products;
using supermarket.ViewModels.ManagerMenu.StoreProducts;
using supermarket.ViewModels.SignIn;
using System;
using System.Windows;
using supermarket.Navigation.ViewModels;
using supermarket.Navigation.WindowViewModels;

namespace supermarket.ViewModels
{
    /* 
     * Controls Main Window (Starting Window)
     * Set navigating of Views as Sign In View, Manager Menu View, Cashier Menu View
     *  and Windows as (Manager Menu) Manager Employees Window, ..., ...
     */
    internal class MainWindowVM : VMNavigator
    {
        private WindowVMNavigator<Main> _windowsNavigator;

        // controllable ViewModels
        private SignInVM _signInVM;
        //// windows opening ViewModels
        private ManagerMenuVM _managerMenuVM;
        private CashierMenuVM _cashierMenuVM;
        ////
        //
        private bool _isEnabled;

        public MainWindowVM(Window window) 
        {
            _isEnabled = true;

            // views navigating setup
            _signInVM = new SignInVM(() => Navigate(VMNavigationTypes.ManagerMenu), () => Navigate(VMNavigationTypes.CashierMenu));
            _managerMenuVM = new ManagerMenuVM(() => Navigate(VMNavigationTypes.SignIn)); 
            _cashierMenuVM = new CashierMenuVM(() => Navigate(VMNavigationTypes.SignIn)); 
            //

            Navigate(VMNavigationTypes.SignIn); // set SignIn View

            Window = window;
            Window.Closed += (object sender, EventArgs e) => Environment.Exit(0);

            _windowsNavigator = new WindowVMNavigator<Main>(new IWindowOpeningVM<Main>[] { _managerMenuVM, _cashierMenuVM });

            _windowsNavigator.SetWay(Main.ManagerEmployees,     GoToManagerEmployees);
            _windowsNavigator.SetWay(Main.ManagerCustomers,     GoToManagerCustomers);
            _windowsNavigator.SetWay(Main.ManagerCategories,    GoToManagerCategories);
            _windowsNavigator.SetWay(Main.ManagerProducts,      GoToManagerProducts);
            _windowsNavigator.SetWay(Main.ManagerStoreProducts, GoToManagerStoreProducts);
        }

        public Window Window { get; private set; }

        public bool IsEnabled 
        { 
            get => _isEnabled; 
            private set
            {
                _isEnabled = value;
                OnPropertyChanged();
            } 
        }

        protected override INavigatableVM CreateViewModel(VMNavigationTypes type)
        {
            switch (type)
            {
                case VMNavigationTypes.SignIn:
                    return _signInVM;
                case VMNavigationTypes.ManagerMenu:
                    return _managerMenuVM;
                case VMNavigationTypes.CashierMenu:
                    return _cashierMenuVM;
                default:
                    return null;
            }
        }

        private void GoToManagerEmployees()
        {
            IsEnabled = false;
            var _windowViewModel = new EmployeesWindowVM();
            _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
        }

        private void GoToManagerCustomers()
        {
            IsEnabled = false;
            var _windowViewModel = new CustomersWindowVM();
            _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
        }

        private void GoToManagerCategories()
        {
            IsEnabled = false;
            var _windowViewModel = new CategoriesWindowVM();
            _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
        }

        private void GoToManagerProducts()
        {
            IsEnabled = false;
            var _windowViewModel = new ProductsWindowVM();
            _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
        }

        private void GoToManagerStoreProducts()
        {
            IsEnabled = false;
            var _windowViewModel = new StoreProductsWindowVM();
            _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
        }
    }
}
