using supermarket.Windows.ManagerMenu.Customers.Changes;

namespace supermarket.ViewModels.ManagerMenu.Customers.Changes
{
    /*
     * Controls AddCustomer Window in Customers of ManagerMenu
     */
    internal class AddCustomerWindowVM : WindowViewModel<AddCustomerWindow, AddCustomerVM>
    {
        public AddCustomerWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
