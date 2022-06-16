using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Sales;

namespace supermarket.ViewModels.ManagerMenu.Sales
{
    internal class SalesWindowVM : WindowViewModel<SalesWindow, SalesVM>
    {
        public SalesWindowVM()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue) 
                {
                    return; 
                }
                // window is shown
                ViewModel.UpdateSales();
            };
        }
    }
}