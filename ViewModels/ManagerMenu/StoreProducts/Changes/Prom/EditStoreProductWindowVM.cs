using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.StoreProducts.Changes.Prom;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.Prom
{
    /*
     * Controls EditEmployee Window in Employees of ManagerMenu
     */
    internal class EditStoreProductWindowVM : WindowViewModel<EditStoreProductWindow, EditStoreProductVM>
    {
        public EditStoreProductWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
