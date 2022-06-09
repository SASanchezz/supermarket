using System;
using System.Windows;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.ViewModels.ManagerMenu.Receipts.Changes;
using supermarket.Windows.ManagerMenu.Receipts;

namespace supermarket.ViewModels.ManagerMenu.Receipts
{
    internal class ReceiptsWindowVM : WindowViewModel<ReceiptsWindow, ReceiptsVM>
    {
        private DetailsReceiptWindowVM _detailsReceiptWindowVM;
        
        public ReceiptsWindowVM()
        {
            _detailsReceiptWindowVM = new DetailsReceiptWindowVM();
            SetUpdatingSystem();
            SetWindowsNavigation();
        }
        
        private void SetUpdatingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                if (!(bool)e.NewValue) return; // window is hiden
                // window is shown
                ViewModel.Reset();
            };
            
            SetUpdatingAfterHiden(_detailsReceiptWindowVM);
        }
        
        private void SetUpdatingAfterHiden<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM) 
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue) return; // window is shown
                // window is hiden
                ViewModel.UpdateReceipts();
            };
        }
        
        private void SetWindowsNavigation()
        {
            var windowsNavigator =
                new WindowVMNavigator<ManagerReceipts>(new IWindowOpeningVM<ManagerReceipts>[] { ViewModel });

            windowsNavigator.SetWay(ManagerReceipts.DetailsReceipt, _detailsReceiptWindowVM.Window, 
                OnOpeningDetailsReceipt);

            SetEnabilitySystem(_detailsReceiptWindowVM);
        }
        
        private void SetEnabilitySystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
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
            ViewModel.SelectedReceipt = null;
        }
    }
}