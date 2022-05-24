using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu
{
    internal class ManagerCustomersWindowViewModel : WindowViewModelNavigator, INavigatableWindowViewModel
    {
        private Window _window;
        private ManagerCustomersViewModel _viewModel;

        public ManagerCustomersWindowViewModel()
        {
            IsEnabled = true;

            _window = new ManagerCustomersWindow
            {
                DataContext = this
            };
            
            _viewModel = new ManagerCustomersViewModel();

            Window.Show();
        }

        public Window Window => _window;
        public WindowTypes WindowType => WindowTypes.ManagerClients;
        public ManagerCustomersViewModel ViewModel { get => _viewModel; }

        protected override INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
