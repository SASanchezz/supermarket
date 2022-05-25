using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu.StoreProducts;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    internal class StoreProductsWindowVM : WindowVMNavigator, INavigatableWindowVM
    {
        private Window _window;
        private StoreProductsVM _viewModel;

        public StoreProductsWindowVM()
        {
            IsEnabled = true;

            _window = new StoreProductsWindow
            {
                DataContext = this
            };

            _viewModel = new StoreProductsVM();

            Window.Show();
        }

        public Window Window => _window;

        public WindowTypes WindowType => WindowTypes.ManagerStoreProducts;

        public StoreProductsVM ViewModel { get => _viewModel; }

        protected override INavigatableWindowVM CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
