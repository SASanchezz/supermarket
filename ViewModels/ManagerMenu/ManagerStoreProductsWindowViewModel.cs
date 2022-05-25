using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu
{
    internal class ManagerStoreProductsWindowViewModel: WindowViewModelNavigator, INavigatableWindowViewModel
    {
        private Window _window;
        private ManagerStoreProductsViewModel _viewModel;

        public ManagerStoreProductsWindowViewModel()
        {
            IsEnabled = true;

            _window = new ManagerStoreProductsWindow
            {
                DataContext = this
            };

            _viewModel = new ManagerStoreProductsViewModel();

            Window.Show();
        }

        public Window Window => _window;

        public WindowTypes WindowType => WindowTypes.ManagerStoreProducts;

        public ManagerStoreProductsViewModel ViewModel { get => _viewModel; }

        protected override INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
