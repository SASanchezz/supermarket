using supermarket.Navigation.ViewsNavigation;
using supermarket.Navigation.WindowsNavigation;
using System;

namespace supermarket.ViewModels
{
    /*
     * Controls Cashier Menu View
     */
    internal class CashierMenuViewModel : IWindowOpeningViewModel, INavigatableViewModel
    {
        private Action _goToSignIn;

        public CashierMenuViewModel(Action goToSignIn)
        {
            _goToSignIn = goToSignIn;
        }

        public ViewTypes ViewType => ViewTypes.CashierMenu;
        public Action<WindowTypes> OpenWindowViewModel { get; set; }
    }
}
