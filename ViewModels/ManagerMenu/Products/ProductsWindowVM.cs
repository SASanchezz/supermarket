using supermarket.Navigation.WindowVM;
using supermarket.Windows.ManagerMenu.Products;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    internal class ProductsWindowVM : WindowVMNavigator<ManagerProducts>, INavigatableWindowVM<Main>
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

        public Main WindowType => Main.ManagerProducts;

        public ProductsVM ViewModel { get => _viewModel; }

        protected override INavigatableWindowVM<ManagerProducts> CreateWindowViewModel(ManagerProducts type)
        {
            throw new NotImplementedException();
        }
    }
}
