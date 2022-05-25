using supermarket.Navigation.ViewsNavigation;
using supermarket.Navigation.WindowsNavigation;
using System;

namespace supermarket.ViewModels.CashierMenu
{
    /*
     * Controls Cashier Menu View
     */
    internal class CashierMenuVM : IWindowOpeningVM, INavigatableVM
    {
        private Action _goToSignIn;

        public CashierMenuVM(Action goToSignIn)
        {
            _goToSignIn = goToSignIn;
        }

        public ViewTypes ViewType => ViewTypes.CashierMenu;
        public Action<WindowTypes> OpenWindowViewModel { get; set; }
    }
}
