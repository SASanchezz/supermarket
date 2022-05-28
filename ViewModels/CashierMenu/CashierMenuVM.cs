using supermarket.Navigation.ViewModels;
using supermarket.Navigation.WindowViewModels;
using System;

namespace supermarket.ViewModels.CashierMenu
{
    /*
     * Controls Cashier Menu View
     */
    internal class CashierMenuVM : IWindowOpeningVM<Main>, INavigatableVM
    {
        private Action _goToSignIn;

        public CashierMenuVM(Action goToSignIn)
        {
            _goToSignIn = goToSignIn;
        }

        public VMNavigationTypes ViewType => VMNavigationTypes.CashierMenu;

        public Action<Main> OpenWindowViewModel { get; set; }
    }
}
