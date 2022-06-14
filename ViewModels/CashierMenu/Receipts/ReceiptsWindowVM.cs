using System;
using System.Windows;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.CashierMenu.Receipts;
using supermarket.Windows.CashierMenu.Receipts.Changes;

namespace supermarket.ViewModels.CashierMenu.Receipts
{
    internal class ReceiptsWindowVM : WindowViewModel<ReceiptsWindow, ReceiptsVM>
    {
        private Changes.AddReceiptWindowVM _addReceiptWindowVM;
        
        public ReceiptsWindowVM()
        {
            _addReceiptWindowVM = new Changes.AddReceiptWindowVM();
            SetUpdatingSystem();
            SetWindowsNavigation();
        }
        
        private void SetUpdatingSystem()
        {
            Window.IsEnabledChanged += (sender, e) =>
            { 
                // window is enabled
                if ((bool)e.NewValue)
                {
                    ViewModel.UpdateReceipts();
                }
            };
            
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue) 
                {
                    return; 
                }
                // window is shown
                ViewModel.UpdateReceipts();
            };
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator =
                new WindowVMNavigator<CashierReceipts>(new IWindowOpeningVM<CashierReceipts>[] { ViewModel });

            windowsNavigator.SetWay(CashierReceipts.AddReceipt, _addReceiptWindowVM.Window,
                OnOpeningAddReceipt);

            SetChangingEnabilityByOpeningAnotherWindow(_addReceiptWindowVM);
        }
        
        private void SetChangingEnabilityByOpeningAnotherWindow<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }

        private void OnOpeningAddReceipt()
        {
            //Nothing
        }
    }
}