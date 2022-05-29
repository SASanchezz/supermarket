using supermarket.Navigation.ViewModels;
using supermarket.Navigation.WindowViewModels;
using System;

namespace supermarket.ViewModels.CashierMenu
{
    /*
     * Controls Cashier Menu View
     */
    internal class CashierMenuVM : NavigatableViewModel, IWindowOpeningVM<Main>
    {
        public Action<Main> OpenWindowViewModel { get; set; }
    }
}
