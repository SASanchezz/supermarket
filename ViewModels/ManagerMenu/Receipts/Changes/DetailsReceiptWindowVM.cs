using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Receipts.Changes;

namespace supermarket.ViewModels.ManagerMenu.Receipts.Changes
{
    internal class DetailsReceiptWindowVM : WindowViewModel<DetailsReceiptWindow, DetailsReceiptVM>
    {
        public DetailsReceiptWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}