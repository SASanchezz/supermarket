using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu.Customers;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Customers
{
    internal class CustomersWindowVM : WindowVMNavigator, INavigatableWindowVM
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
        public WindowTypes WindowType => WindowTypes.ManagerCustomers;
        public CustomersVM ViewModel { get => _viewModel; }

        protected override INavigatableWindowVM CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
