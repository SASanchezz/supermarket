using supermarket.Navigation.WindowVM;
using supermarket.Windows.ManagerMenu.Categories;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Categories
{
    internal class CategoriesWindowVM : WindowVMNavigator<ManagerCategories>, INavigatableWindowVM<Main>
    {
        private Window _window;
        private CategoriesVM _viewModel;

        public CategoriesWindowVM()
        {
            IsEnabled = true;

            _window = new CategoriesWindow
            {
                DataContext = this
            };

            _viewModel = new CategoriesVM();

            Window.Show();
        }

        public Window Window => _window;

        public Main WindowType => Main.ManagerCategories;

        public CategoriesVM ViewModel => _viewModel; 

        protected override INavigatableWindowVM<ManagerCategories> CreateWindowViewModel(ManagerCategories type)
        {
            throw new NotImplementedException();
        }
    }
}
