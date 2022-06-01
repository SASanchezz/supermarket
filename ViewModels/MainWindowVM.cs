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
            _employeesWindowVM = new EmployeesWindowVM();
            _customersWindowVM = new CustomersWindowVM();
            _categoriesWindowVM = new CategoriesWindowVM();
            _productsWindowVM = new ProductsWindowVM();
            _storeProductsWindowVM = new StoreProductsWindowVM();
            
            _signInVM = new SignInVM();
            _managerMenuVM = new ManagerMenuVM();
            _cashierMenuVM = new CashierMenuVM();
            
            SetWindowsNavigation();
            SetViewsNavigation();

            Window.Closed += (sender, e) => Environment.Exit(0);
        }

        public NavigatableViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator = new WindowVMNavigator<Main>(new IWindowOpeningVM<Main>[] { _managerMenuVM, _cashierMenuVM });

            windowsNavigator.SetWay(Main.ManagerEmployees, _employeesWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerCustomers, _customersWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerCategories, _categoriesWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerProducts, _productsWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerStoreProducts, _storeProductsWindowVM.Window);

            SetVisibilitySystem(_employeesWindowVM);
            SetVisibilitySystem(_customersWindowVM);
            SetVisibilitySystem(_categoriesWindowVM);
            SetVisibilitySystem(_productsWindowVM);
            SetVisibilitySystem(_storeProductsWindowVM);
        }

        private void SetViewsNavigation()
        {
            var viewsNavigator = new VMNavigator((vm) => CurrentViewModel = vm);
            
            viewsNavigator.SetWay(VMNavigationTypes.SignIn, _signInVM);
            viewsNavigator.SetWay(VMNavigationTypes.ManagerMenu, _managerMenuVM);
            viewsNavigator.SetWay(VMNavigationTypes.CashierMenu, _cashierMenuVM);

            viewsNavigator.Navigate(VMNavigationTypes.SignIn);
        }

        private void SetVisibilitySystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
    }
}
