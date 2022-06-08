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
using supermarket.ViewModels.ManagerMenu.Receipts;
using supermarket.ViewModels.ManagerMenu.Sales;
using supermarket.Windows.ManagerMenu.Receipts;

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
        private ReceiptsWindowVM _receiptsWindowVM;
        private SalesWindowVM _salesWindowVM;

        // controllable ViewModels
        private SignInVM _signInVM;
        //// windows opening ViewModels
        private ManagerMenuVM _managerMenuVM;
        private CashierMenuVM _cashierMenuVM;
        ////
        //

        private NavigatableViewModel<MainViewsTypes> _currentViewModel;

        public MainWindowVM(MainWindow window) : base(window)
        {
            _employeesWindowVM = new EmployeesWindowVM();
            _customersWindowVM = new CustomersWindowVM();
            _categoriesWindowVM = new CategoriesWindowVM();
            _productsWindowVM = new ProductsWindowVM();
            _storeProductsWindowVM = new StoreProductsWindowVM();
            _receiptsWindowVM = new ReceiptsWindowVM();
            _salesWindowVM = new SalesWindowVM();
            
            _signInVM = new SignInVM();
            _managerMenuVM = new ManagerMenuVM();
            _cashierMenuVM = new CashierMenuVM();
            
            SetWindowsNavigation();
            SetViewsNavigation();

            Window.Closing += (sender, e) => Environment.Exit(0);
        }

        public NavigatableViewModel<MainViewsTypes> CurrentViewModel
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
            windowsNavigator.SetWay(Main.ManagerReceipts, _receiptsWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerSales, _salesWindowVM.Window);

            SetEnabilitySystem(_employeesWindowVM);
            SetEnabilitySystem(_customersWindowVM);
            SetEnabilitySystem(_categoriesWindowVM);
            SetEnabilitySystem(_productsWindowVM);
            SetEnabilitySystem(_storeProductsWindowVM);
            SetEnabilitySystem(_receiptsWindowVM);
            SetEnabilitySystem(_salesWindowVM);
        }

        private void SetViewsNavigation()
        {
            var viewsNavigator = new VMNavigator<MainViewsTypes>((vm) => CurrentViewModel = vm);
            
            viewsNavigator.SetWay(MainViewsTypes.SignIn, _signInVM);
            viewsNavigator.SetWay(MainViewsTypes.ManagerMenu, _managerMenuVM);
            viewsNavigator.SetWay(MainViewsTypes.CashierMenu, _cashierMenuVM);

            viewsNavigator.Navigate(MainViewsTypes.SignIn);
        }

        private void SetEnabilitySystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
    }
}
