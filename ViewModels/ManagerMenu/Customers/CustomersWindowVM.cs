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

            SetWindowsNavigation();

            Window.Closed += (sender, e) =>
            {
                _addCustomerWindowVM.Window.Close();
                _editCustomerWindowVM.Window.Close();
            };

            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator = new WindowVMNavigator<ManagerCustomers>(new IWindowOpeningVM<ManagerCustomers>[] { ViewModel });

            windowsNavigator.SetWay(ManagerCustomers.AddCustomer, _addCustomerWindowVM.Window, GoToAddCustomer);
            windowsNavigator.SetWay(ManagerCustomers.EditCustomer, _editCustomerWindowVM.Window, GoToEditCustomer);
            
            SetVisibilitySystem(_addCustomerWindowVM);
            SetVisibilitySystem(_editCustomerWindowVM);
        }

        private void GoToAddCustomer()
        {
            SetDefaultClosedEventHandler(_addCustomerWindowVM);
        }

        private void GoToEditCustomer()
        {
            if (ViewModel.SelectedCustomer == null)
            {
                throw new Exception("No selected item");
            }

            SetDefaultClosedEventHandler(_editCustomerWindowVM);

            _editCustomerWindowVM.ViewModel.SetData(ViewModel.SelectedCustomer);
            ViewModel.SelectedCustomer = null;
        }

        private void SetVisibilitySystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }

        private void SetDefaultClosedEventHandler<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.Closing += (sender, e) => ViewModel.UpdateCustomers();
        }
    }
}
