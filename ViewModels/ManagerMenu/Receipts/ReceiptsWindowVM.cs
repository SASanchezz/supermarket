using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Receipts;

namespace supermarket.ViewModels.ManagerMenu.Receipts
{
    internal class ReceiptsWindowVM : WindowViewModel<ReceiptsWindow, ReceiptsVM>
    {
        public ReceiptsWindowVM()
        {
            SetUpdatingSystem();
            ViewModel.Close = Window.Close;
        }
        
        private void SetUpdatingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                if (!(bool)e.NewValue) return; // window is hiden
                // window is shown
                ViewModel.UpdateReceipts();
                ViewModel.SetDefaultPrintDates();
            };
            // SetUpdatingAfterHiden(_addEmployeeWindowVM);
            // SetUpdatingAfterHiden(_editEmployeeWindowVM);
        }
    }
}