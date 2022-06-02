using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.StoreProducts.Changes.NonProm;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.NonProm
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
