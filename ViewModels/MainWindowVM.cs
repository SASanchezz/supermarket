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
using M = supermarket.ViewModels.ManagerMenu;
using C = supermarket.ViewModels.CashierMenu;

namespace supermarket.ViewModels
{
    /* 
     * Controls Main Window (Starting Window)
     * Set navigating of Views as Sign In View, Manager Menu View, Cashier Menu View
     *  and Windows as (Manager Menu) Manager Employees Window, ..., ...
     */
    internal class MainWindowVM : WindowViewModel<MainWindow>
    {
        private EmployeesWindowVM _m_employeesWindowVM;    
        private CustomersWindowVM _m_customersWindowVM;
        private CategoriesWindowVM _m_categoriesWindowVM;
        private M.Products.ProductsWindowVM _m_productsWindowVM;
        private StoreProductsWindowVM _m_storeProductsWindowVM;
        private ReceiptsWindowVM _m_receiptsWindowVM;
        private SalesWindowVM _m_salesWindowVM;

        private C.Products.ProductsWindowVM _c_productsWindowVM;
        private C.StoreProducts.StoreProductsWindowVM _c_storeProductsWindowVM;
        private C.Customers.CustomersWindowVM _c_customersWindowVM;
        private C.Receipts.ReceiptsWindowVM _c_receiptsWindowVM;

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
            _m_employeesWindowVM = new EmployeesWindowVM();
            _m_customersWindowVM = new CustomersWindowVM();
            _m_categoriesWindowVM = new CategoriesWindowVM();
            _m_productsWindowVM = new M.Products.ProductsWindowVM();
            _m_storeProductsWindowVM = new StoreProductsWindowVM();
            _m_receiptsWindowVM = new ReceiptsWindowVM();
            _m_salesWindowVM = new SalesWindowVM();

            _c_productsWindowVM = new C.Products.ProductsWindowVM();
            _c_storeProductsWindowVM = new C.StoreProducts.StoreProductsWindowVM();
            _c_customersWindowVM = new C.Customers.CustomersWindowVM();
            _c_receiptsWindowVM = new C.Receipts.ReceiptsWindowVM();
            
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

            windowsNavigator.SetWay(Main.ManagerEmployees, _m_employeesWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerCustomers, _m_customersWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerCategories, _m_categoriesWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerProducts, _m_productsWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerStoreProducts, _m_storeProductsWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerReceipts, _m_receiptsWindowVM.Window);
            windowsNavigator.SetWay(Main.ManagerSales, _m_salesWindowVM.Window);
            
            windowsNavigator.SetWay(Main.CashierProducts, _c_productsWindowVM.Window);
            windowsNavigator.SetWay(Main.CashierStoreProducts, _c_storeProductsWindowVM.Window);
            windowsNavigator.SetWay(Main.CashierCustomers, _c_customersWindowVM.Window);
            windowsNavigator.SetWay(Main.CashierChecks, _c_receiptsWindowVM.Window);

            SetEnabilitySystem(_m_employeesWindowVM);
            SetEnabilitySystem(_m_customersWindowVM);
            SetEnabilitySystem(_m_categoriesWindowVM);
            SetEnabilitySystem(_m_productsWindowVM);
            SetEnabilitySystem(_m_storeProductsWindowVM);
            SetEnabilitySystem(_m_receiptsWindowVM);
            SetEnabilitySystem(_m_salesWindowVM);
            
            SetEnabilitySystem(_c_productsWindowVM);
            SetEnabilitySystem(_c_storeProductsWindowVM);
            SetEnabilitySystem(_c_customersWindowVM);
            SetEnabilitySystem(_c_receiptsWindowVM);
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
