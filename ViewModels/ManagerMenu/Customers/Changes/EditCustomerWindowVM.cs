using supermarket.Windows.ManagerMenu.Employees.Changes;

namespace supermarket.ViewModels.ManagerMenu.Customers.Changes
{
    /*
     * Controls EditCustomer Window in Customer of ManagerMenu
     */
    internal class EditCustomerWindowVM : WindowViewModel<EditEmployeeWindow, EditCustomerVM>
    {
        public EditCustomerWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
