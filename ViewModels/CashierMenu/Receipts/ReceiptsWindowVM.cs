using System;
using System.Windows;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.CashierMenu.Receipts;
using supermarket.Windows.CashierMenu.Receipts.Changes;
using supermarket.ViewModels.ManagerMenu.Receipts.Changes;

namespace supermarket.ViewModels.CashierMenu.Receipts
{
    internal class ReceiptsWindowVM : WindowViewModel<ReceiptsWindow, ReceiptsVM>
    {
        private DetailsReceiptWindowVM _detailsReceiptWindowVM;
        private Changes.AddReceiptWindowVM _addReceiptWindowVM;
        
        public ReceiptsWindowVM()
        {
            _detailsReceiptWindowVM = new DetailsReceiptWindowVM();
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

            windowsNavigator.SetWay(CashierReceipts.DetailsReceipt, _detailsReceiptWindowVM.Window,
                OnOpeningDetailsReceipt);


            SetChangingEnabilityByOpeningAnotherWindow(_addReceiptWindowVM);
            SetChangingEnabilityByOpeningAnotherWindow(_detailsReceiptWindowVM);
        }
        
        private void SetChangingEnabilityByOpeningAnotherWindow<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }

        private void OnOpeningDetailsReceipt()
        {
            if (ViewModel.SelectedReceipt == null)
            {
                throw new Exception("No selected item");
            }

            _detailsReceiptWindowVM.ViewModel.SetData(ViewModel.SelectedReceipt);
        }

        private void OnOpeningAddReceipt()
        {
            //Nothing
        }
    }
}