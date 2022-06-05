using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.StoreProducts.Changes.Prom;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.Prom
{
    /*
     * Controls EditStoreProduct Window in StoreProducts of ManagerMenu
     */
    internal class EditStoreProductWindowVM : WindowViewModel<EditStoreProductWindow, EditStoreProductVM>
    {
        public EditStoreProductWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
