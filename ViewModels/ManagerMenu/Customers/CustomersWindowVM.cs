using supermarket.Windows.ManagerMenu.Customers;

namespace supermarket.ViewModels.ManagerMenu.Customers
{
    internal class CustomersWindowVM : WindowViewModel<CustomersWindow, CustomersVM>
    {
        public CustomersWindowVM()
        {
            IsEnabled = true;

            Window.Show();
        }
    }
}
