using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.CashierMenu.Customers.Changes;

namespace supermarket.ViewModels.CashierMenu.Customers.Changes
{
    /*
     * Controls AddCustomer Window in Customers of ManagerMenu
     */
    internal class AddCustomerWindowVM : WindowViewModel<AddCustomerWindow, AddCustomerVM>
    {
        public AddCustomerWindowVM()
        {
            SetResettingSystem();
        }
        
        private void SetResettingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue)
                {
                    return;
                }
            
                // window is shown
                ViewModel.Reset();
            };
        }
    }
}
