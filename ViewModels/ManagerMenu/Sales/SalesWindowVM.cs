using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Sales;

namespace supermarket.ViewModels.ManagerMenu.Sales
{
    internal class SalesWindowVM : WindowViewModel<SalesWindow, SalesVM>
    {
        public SalesWindowVM()
        {
            SetUpdatingSystem();
        }
        
        private void SetUpdatingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                if (!(bool)e.NewValue) return; // window is hiden
                // window is shown
                ViewModel.Reset();
            };
        }
    }
}