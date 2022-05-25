using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu.Products;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    internal class ProductsWindowVM : WindowVMNavigator, INavigatableWindowVM
    {
        private Window _window;
        private ProductsVM _viewModel;

        public ProductsWindowVM()
        {
            IsEnabled = true;

            _window = new ProductsWindow
            {
                DataContext = this
            };

            _viewModel = new ProductsVM();

            Window.Show();
        }

        public Window Window => _window;

        public WindowTypes WindowType => WindowTypes.ManagerProducts;

        public ProductsVM ViewModel { get => _viewModel; }

        protected override INavigatableWindowVM CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
