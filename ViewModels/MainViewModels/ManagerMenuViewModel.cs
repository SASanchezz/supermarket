using supermarket.Navigation.ViewsNavigation;
using supermarket.Utils;
using System;
using supermarket.Navigation.WindowsNavigation;

namespace supermarket.ViewModels
{
    /*
     * Controls Manager Menu View
     */
    internal class ManagerMenuViewModel : IWindowOpeningViewModel, INavigatableViewModel
    {
        private RelayCommand<object> _openEmployeesWindowCommand; // +
        private RelayCommand<object> _openProductsWindowCommand;
        private RelayCommand<object> _openCategoriesWindowCommand;
        private RelayCommand<object> _openClientsWindowCommand;
        private RelayCommand<object> _openStoreProductsWindowCommand;

        private RelayCommand<object> _goToSignInCommand; // +

        private Action _goToSignIn;

        public ManagerMenuViewModel(Action goToSignIn)
        {
            _goToSignIn = goToSignIn;
        }

        public Action<WindowTypes> OpenWindowViewModel { get; set; }
        public ViewTypes ViewType => ViewTypes.ManagerMenu;
        public RelayCommand<object> OpenEmployeesWindowCommand
        {
            get
            {
                return _openEmployeesWindowCommand ??= new RelayCommand<object>(_ => OpenEmployeesWindow());
            }
        }
        public RelayCommand<object> OpenProductsWindowCommand
        {
            get
            {
                return _openProductsWindowCommand ??= new RelayCommand<object>(_ => OpenEmployeesWindow());
            }
        }
        public RelayCommand<object> OpenCategoriesWindowCommand
        {
            get
            {
                return _openCategoriesWindowCommand ??= new RelayCommand<object>(_ => OpenEmployeesWindow());
            }
        }
        public RelayCommand<object> OpenClientsWindowCommand
        {
            get
            {
                return _openClientsWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel?.Invoke(WindowTypes.ManagerClients));
            }
        }
        public RelayCommand<object> OpenStoreProductsWindowCommand
        {
            get
            {
                return _openStoreProductsWindowCommand ??= new RelayCommand<object>(_ => OpenEmployeesWindow());
            }
        }
        public RelayCommand<object> GoToSignInCommand
        {
            get
            {
                return _goToSignInCommand ??= new RelayCommand<object>(_ => _goToSignIn?.Invoke());
            }
        }

        private void OpenEmployeesWindow()
        {
            OpenWindowViewModel?.Invoke(WindowTypes.ManagerEmployees);
        }
    }
}
