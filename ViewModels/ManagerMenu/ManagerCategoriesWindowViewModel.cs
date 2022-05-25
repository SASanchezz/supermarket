using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu
{
    internal class ManagerCategoriesWindowViewModel : WindowViewModelNavigator, INavigatableWindowViewModel
    {
        private Window _window;
        private ManagerCategoriesViewModel _viewModel;

        public ManagerCategoriesWindowViewModel()
        {
            IsEnabled = true;

            _window = new ManagerCategoriesWindow
            {
                DataContext = this
            };

            _viewModel = new ManagerCategoriesViewModel();

            Window.Show();
        }

        public Window Window => _window;

        public WindowTypes WindowType => WindowTypes.ManagerCategories;

        public ManagerCategoriesViewModel ViewModel { get => _viewModel; }

        protected override INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
