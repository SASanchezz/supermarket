using supermarket.Navigation;
using supermarket.Navigation.ViewsNavigation;
using supermarket.Navigation.WindowsNavigation;
using supermarket.ViewModels.ManagerMenu;
using System;

namespace supermarket.ViewModels
{
    /* 
     * Controls Main Window (Starting Window)
     * Set navigating of Views as Sign In View, Manager Menu View, Cashier Menu View
     *  and Windows as (Manager Menu) Manager Employees Window, ..., ...
     */
    internal class MainWindowViewModel : WindowViewModelsAndViewModelsNavigator
    {
        // controllable ViewModels
        private SignInViewModel _signInViewModel;
            // windows opening ViewModels
        private ManagerMenuViewModel _managerMenuViewModel;
        private CashierMenuViewModel _cashierMenuViewModel;
            //
        //
        public MainWindowViewModel() 
        {
            IsEnabled = true;

            // views navigating setup
            _signInViewModel = new SignInViewModel(() => Navigate(ViewTypes.ManagerMenu), () => Navigate(ViewTypes.CashierMenu));
            _managerMenuViewModel = new ManagerMenuViewModel(() => Navigate(ViewTypes.SignIn)); 
            _cashierMenuViewModel = new CashierMenuViewModel(() => Navigate(ViewTypes.SignIn)); 
            //

            Navigate(ViewTypes.SignIn); // set SignIn View

            SetWindowOpeningViewModel(new IWindowOpeningViewModel[] { _managerMenuViewModel, _cashierMenuViewModel });
        }

        protected override INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type)
        {
            INavigatableWindowViewModel _windowViewModel;
            switch (type)
            {
                case WindowTypes.ManagerEmployees:
                    _windowViewModel = new ManagerEmployeesWindowViewModel();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                case WindowTypes.ManagerClients:
                    _windowViewModel = new ManagerCustomersWindowViewModel();
                    _windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                    return _windowViewModel;
                default:
                    return null;
            }
        }

        protected override INavigatableViewModel CreateViewModel(ViewTypes type)
        {
            switch (type)
            {
                case ViewTypes.SignIn:
                    return _signInViewModel;
                case ViewTypes.ManagerMenu:
                    return _managerMenuViewModel;
                case ViewTypes.CashierMenu:
                    return _cashierMenuViewModel;
                default:
                    return null;
            }
        }
    }
}
