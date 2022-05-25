using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu.Categories;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Categories
{
    internal class CategoriesWindowVM : WindowVMNavigator, INavigatableWindowVM
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

        public WindowTypes WindowType => WindowTypes.ManagerCategories;

        public CategoriesVM ViewModel { get => _viewModel; }

        protected override INavigatableWindowVM CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
