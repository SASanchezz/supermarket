using supermarket.Windows.ManagerMenu.Employees.Changes;

namespace supermarket.ViewModels.ManagerMenu.Customers.Changes
{
    /*
     * Controls AddCustomer Window in Customers of ManagerMenu
     */
    internal class AddCustomerWindowVM : WindowViewModel<AddEmployeeWindow, AddCustomerVM>
    {
        public AddCustomerWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
