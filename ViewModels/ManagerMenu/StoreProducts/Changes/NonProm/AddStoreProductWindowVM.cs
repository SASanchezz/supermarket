using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.StoreProducts.Changes.NonProm;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.NonProm
{
    /*
     * Controls AddEmployee Window in Employees of ManagerMenu
     */
    internal class AddStoreProdcutWindowVM : WindowViewModel<AddStoreProductWindow, AddStoreProductVM>
    {
        public AddStoreProdcutWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
