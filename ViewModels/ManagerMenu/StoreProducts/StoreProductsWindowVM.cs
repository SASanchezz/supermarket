using supermarket.Navigation.WindowVM;
using supermarket.Windows.ManagerMenu.StoreProducts;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    internal class StoreProductsWindowVM : WindowVMNavigator<ManagerStoreProducts>, INavigatableWindowVM<Main>
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

        public Main WindowType => Main.ManagerStoreProducts; 

        public StoreProductsVM ViewModel => _viewModel; 

        protected override INavigatableWindowVM<ManagerStoreProducts> CreateWindowViewModel(ManagerStoreProducts type)
        {
            throw new NotImplementedException();
        }
    }
}
