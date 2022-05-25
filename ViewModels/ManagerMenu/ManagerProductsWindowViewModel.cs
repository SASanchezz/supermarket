using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu
{
    internal class ManagerProductsWindowViewModel : WindowViewModelNavigator, INavigatableWindowViewModel
    {
        private Window _window;
        private ManagerProductsViewModel _viewModel;

        public ManagerProductsWindowViewModel()
        {
            IsEnabled = true;

            _window = new ManagerProductsWindow
            {
                DataContext = this
            };

            _viewModel = new ManagerProductsViewModel();

            Window.Show();
        }

        public Window Window => _window;

        public WindowTypes WindowType => WindowTypes.ManagerProducts;

        public ManagerProductsViewModel ViewModel { get => _viewModel; }

        protected override INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
