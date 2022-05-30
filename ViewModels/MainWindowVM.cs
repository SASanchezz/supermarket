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
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels
{
    /* 
     * Controls Main Window (Starting Window)
     * Set navigating of Views as Sign In View, Manager Menu View, Cashier Menu View
     *  and Windows as (Manager Menu) Manager Employees Window, ..., ...
     */
    internal class MainWindowVM : WindowViewModel<MainWindow>
    {
        private WindowVMNavigator<Main> _windowsNavigator;
        private VMNavigator _viewsNavigator;

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

        private NavigatableViewModel _currentViewModel;

        public MainWindowVM(MainWindow window) : base(window)
        {
            _signInVM = new SignInVM();
            _managerMenuVM = new ManagerMenuVM();
            _cashierMenuVM = new CashierMenuVM();

            // views navigation setup
            _viewsNavigator = new VMNavigator((vm) => CurrentViewModel = vm);
            
            _viewsNavigator.SetWay(VMNavigationTypes.SignIn, _signInVM);
            _viewsNavigator.SetWay(VMNavigationTypes.ManagerMenu, _managerMenuVM);
            _viewsNavigator.SetWay(VMNavigationTypes.CashierMenu, _cashierMenuVM);

            _viewsNavigator.Navigate(VMNavigationTypes.SignIn);
            //

            _employeesWindowVM = new EmployeesWindowVM();
            _customersWindowVM = new CustomersWindowVM();
            _categoriesWindowVM = new CategoriesWindowVM();
            _productsWindowVM = new ProductsWindowVM();
            _storeProductsWindowVM = new StoreProductsWindowVM();

            // windows navigation setup
            _windowsNavigator = new WindowVMNavigator<Main>(new IWindowOpeningVM<Main>[] { _managerMenuVM, _cashierMenuVM });

            _windowsNavigator.SetWay(Main.ManagerEmployees, _employeesWindowVM.Window);
            _windowsNavigator.SetWay(Main.ManagerCustomers, _customersWindowVM.Window);
            _windowsNavigator.SetWay(Main.ManagerCategories, _categoriesWindowVM.Window);
            _windowsNavigator.SetWay(Main.ManagerProducts, _productsWindowVM.Window);
            _windowsNavigator.SetWay(Main.ManagerStoreProducts, _storeProductsWindowVM.Window);

            SetVisibilitySystem(_employeesWindowVM);
            SetVisibilitySystem(_customersWindowVM);
            SetVisibilitySystem(_categoriesWindowVM);
            SetVisibilitySystem(_productsWindowVM);
            SetVisibilitySystem(_storeProductsWindowVM);
            //

            Window.Closed += (sender, e) => Environment.Exit(0);
        }

        public NavigatableViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
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
