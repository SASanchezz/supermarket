using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.CashierMenu.StoreProducts;

namespace supermarket.ViewModels.CashierMenu.StoreProducts
{
    internal class StoreProductsWindowVM : WindowViewModel<StoreProductsWindow, StoreProductsVM>
    {
        public StoreProductsWindowVM()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue)
                {
                    return;
                }
                // window is shown
                ViewModel.UpdateStoreProducts();
            };
        }
    }
    
}
