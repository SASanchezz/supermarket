using supermarket.Windows.ManagerMenu.Customers.Changes;

namespace supermarket.ViewModels.ManagerMenu.Customers.Changes
{
    /*
     * Controls EditCustomer Window in Customer of ManagerMenu
     */
    internal class EditCustomerWindowVM : WindowViewModel<EditCustomerWindow, EditCustomerVM>
    {
        public EditCustomerWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
