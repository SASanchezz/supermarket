using supermarket.Windows.ManagerMenu.Customers;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.ManagerMenu.Customers.Changes;
using System;
using System.Windows;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Customers
{
    /* 
     * Controls ManagerCustomers Window
     * Set navigating Windows as ManagerAddCustomer Window, ManagerEditCustomer Window
     */
    internal class CustomersWindowVM : WindowViewModel<CustomersWindow, CustomersVM>
    {
        private AddCustomerWindowVM _addCustomerWindowVM;
        private EditCustomerWindowVM _editCustomerWindowVM;

        public CustomersWindowVM()
        {
            _addCustomerWindowVM = new AddCustomerWindowVM();
            _editCustomerWindowVM = new EditCustomerWindowVM();

            SetUpdatingSystem();
            SetWindowsNavigation();

            Window.Closed += (sender, e) =>
            {
                _addCustomerWindowVM.Window.Close();
                _editCustomerWindowVM.Window.Close();
            };
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator = 
                new WindowVMNavigator<ManagerCustomers>(new IWindowOpeningVM<ManagerCustomers>[] { ViewModel });

            windowsNavigator.SetWay(ManagerCustomers.AddCustomer, _addCustomerWindowVM.Window);
            windowsNavigator.SetWay(ManagerCustomers.EditCustomer, _editCustomerWindowVM.Window, 
                OnOpeningEditCustomerHandler);
            
            SetEnabilitySystem(_addCustomerWindowVM);
            SetEnabilitySystem(_editCustomerWindowVM);
        }
        
        private void OnOpeningEditCustomerHandler()
        {
            if (ViewModel.SelectedCustomer == null)
            {
                throw new Exception("No selected item");
            }

            _editCustomerWindowVM.ViewModel.SetData(ViewModel.SelectedCustomer);
        }

        private void SetUpdatingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue) return;
                // window is shown
                ViewModel.Reset();
            };
            SetUpdatingAfterHiden(_addCustomerWindowVM);
            SetUpdatingAfterHiden(_editCustomerWindowVM);
        }
        
        private void SetUpdatingAfterHiden<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM) 
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue) return; // window is shown
                // window is hiden
                ViewModel.UpdateCustomers();
            };
        }
        
        private void SetEnabilitySystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
    }
}
