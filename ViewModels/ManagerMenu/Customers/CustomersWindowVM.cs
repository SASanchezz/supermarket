using supermarket.Navigation.WindowVM;
using supermarket.Windows.ManagerMenu.Customers;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Customers
{
    internal class CustomersWindowVM : WindowVMNavigator<ManagerCustomers>, INavigatableWindowVM<Main>
    {
        private Window _window;
        private CustomersVM _viewModel;

        public CustomersWindowVM()
        {
            IsEnabled = true;

            _window = new CustomersWindow
            {
                DataContext = this
            };

            _viewModel = new CustomersVM();

            Window.Show();
        }

        public Window Window => _window;

        public Main WindowType => Main.ManagerCustomers;

        public CustomersVM ViewModel => _viewModel; 

        protected override void CreateWindowViewModel(ManagerCustomers type)
        {
            throw new NotImplementedException();
        }
    }
}
