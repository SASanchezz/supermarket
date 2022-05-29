using supermarket.Windows.ManagerMenu.Customers;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.ManagerMenu.Customers.Changes;
using System;
using System.Windows;
namespace supermarket.ViewModels.ManagerMenu.Customers
{
    /* 
     * Controls ManagerCustomers Window
     * Set navigating Windows as ManagerAddCustomer Window, ManagerEditCustomer Window
     */
    internal class CustomersWindowVM : WindowViewModel<CustomersWindow, CustomersVM>
    {
        private WindowVMNavigator<ManagerCustomers> _windowsNavigator;

        private AddCustomerWindowVM _addCustomerWindowVM;
        private EditCustomerWindowVM _editCustomerWindowVM;

        public CustomersWindowVM()
        {
            _addCustomerWindowVM = new AddCustomerWindowVM();
            _editCustomerWindowVM = new EditCustomerWindowVM();

            _windowsNavigator = new WindowVMNavigator<ManagerCustomers>(new IWindowOpeningVM<ManagerCustomers>[] { ViewModel });

            _windowsNavigator.SetWay(ManagerCustomers.AddCustomer, _addCustomerWindowVM.Window);
            _windowsNavigator.SetWay(ManagerCustomers.EditCustomer, _editCustomerWindowVM.Window);

            Window.Closed += (object sender, EventArgs e) =>
            {
                _addCustomerWindowVM.Window.Close();
                _editCustomerWindowVM.Window.Close();
            };

            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;
        }

        private void GoToAddCustomer()
        {
            SetDefaultClosedEventHandler(_addCustomerWindowVM);
            _addCustomerWindowVM.Window.Show();
        }

        private void GoToEditCustomer()
        {
            try
            {
                if (ViewModel.SelectedCustomer == null)
                {
                    throw new Exception("No selected item");
                }

                SetDefaultClosedEventHandler(_editCustomerWindowVM);

                _editCustomerWindowVM.ViewModel.SetData(ViewModel.SelectedCustomer);
                ViewModel.SelectedCustomer = null;
                _editCustomerWindowVM.Window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void SetDefaultClosedEventHandler<WindowType, VMType>(WindowViewModel<WindowType, VMType> windowVM)
            where WindowType : Window, new()
            where VMType : ViewModel, new()
        {
            windowVM.Window.Closing += (sender, e) => ViewModel.UpdateCustomers();
        }
    }
}
