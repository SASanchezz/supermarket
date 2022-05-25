﻿using supermarket.Navigation;
using supermarket.Navigation.VM;
using supermarket.ViewModels.CashierMenu;
using supermarket.ViewModels.ManagerMenu;
using supermarket.ViewModels.ManagerMenu.Categories;
using supermarket.ViewModels.ManagerMenu.Customers;
using supermarket.ViewModels.ManagerMenu.Employees;
using supermarket.ViewModels.ManagerMenu.Products;
using supermarket.ViewModels.ManagerMenu.StoreProducts;
using supermarket.ViewModels.SignIn;
using supermarket.Navigation.WindowVM;
using System;

namespace supermarket.ViewModels
{
    /* 
     * Controls Main Window (Starting Window)
     * Set navigating of Views as Sign In View, Manager Menu View, Cashier Menu View
     *  and Windows as (Manager Menu) Manager Employees Window, ..., ...
     */
    internal class MainWindowVM : WindowVMAndVMNavigator<Main>
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
            _signInVM = new SignInVM(() => Navigate(VMNavigationTypes.ManagerMenu), () => Navigate(VMNavigationTypes.CashierMenu));
            _managerMenuVM = new ManagerMenuVM(() => Navigate(VMNavigationTypes.SignIn)); 
            _cashierMenuVM = new CashierMenuVM(() => Navigate(VMNavigationTypes.SignIn)); 
            //

            Navigate(VMNavigationTypes.SignIn); // set SignIn View

            SetWindowOpeningVM(new IWindowOpeningVM<Main>[] { _managerMenuVM, _cashierMenuVM });
        }

        protected override INavigatableWindowVM<Main> CreateWindowViewModel(Main type)
        {
            INavigatableWindowVM<Main> _windowViewModel;
            switch (type)
            {
                case Main.ManagerEmployees:
                    _windowViewModel = new EmployeesWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case Main.ManagerCustomers:
                    _windowViewModel = new CustomersWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case Main.ManagerCategories:
                    _windowViewModel = new CategoriesWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case Main.ManagerProducts:
                    _windowViewModel = new ProductsWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case Main.ManagerStoreProducts:
                    _windowViewModel = new StoreProductsWindowVM();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                default:
                    return null;
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
    }
}