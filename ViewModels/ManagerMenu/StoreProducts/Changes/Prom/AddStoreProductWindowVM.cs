using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.StoreProducts.Changes.Prom;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.Prom
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
