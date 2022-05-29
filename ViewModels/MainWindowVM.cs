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

        private EmployeesWindowVM _employeesWindowVM;    
        private CustomersWindowVM _customersWindowVM;
        private CategoriesWindowVM _categoriesWindowVM;
        private ProductsWindowVM _productsWindowVM;
        private StoreProductsWindowVM _storeProductsWindowVM;

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
            Window = window;
            Window.Closed += (object sender, EventArgs e) => Environment.Exit(0);

            // views navigating setup
            _signInVM = new SignInVM(() => Navigate(VMNavigationTypes.ManagerMenu), () => Navigate(VMNavigationTypes.CashierMenu));
            _managerMenuVM = new ManagerMenuVM(() => Navigate(VMNavigationTypes.SignIn));
            _cashierMenuVM = new CashierMenuVM(() => Navigate(VMNavigationTypes.SignIn));
            //

            Navigate(VMNavigationTypes.SignIn); // set SignIn View

            _employeesWindowVM = new EmployeesWindowVM();
            _customersWindowVM = new CustomersWindowVM();
            _categoriesWindowVM = new CategoriesWindowVM();
            _productsWindowVM = new ProductsWindowVM();
            _storeProductsWindowVM = new StoreProductsWindowVM();

            SetVisibilitySystem(_employeesWindowVM);
            SetVisibilitySystem(_customersWindowVM);
            SetVisibilitySystem(_categoriesWindowVM);
            SetVisibilitySystem(_productsWindowVM);
            SetVisibilitySystem(_storeProductsWindowVM);

            _windowsNavigator = new WindowVMNavigator<Main>(new IWindowOpeningVM<Main>[] { _managerMenuVM, _cashierMenuVM });

            _windowsNavigator.SetWay(Main.ManagerEmployees, () => _employeesWindowVM.Window.Show());
            _windowsNavigator.SetWay(Main.ManagerCustomers, () => _customersWindowVM.Window.Show());
            _windowsNavigator.SetWay(Main.ManagerCategories, () => _categoriesWindowVM.Window.Show());
            _windowsNavigator.SetWay(Main.ManagerProducts, () => _productsWindowVM.Window.Show());
            _windowsNavigator.SetWay(Main.ManagerStoreProducts, () => _storeProductsWindowVM.Window.Show());
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

        private void SetVisibilitySystem<WindowType, VMType>(WindowViewModel<WindowType, VMType> windowVM)
            where WindowType : Window, new()
            where VMType : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
    }
}
