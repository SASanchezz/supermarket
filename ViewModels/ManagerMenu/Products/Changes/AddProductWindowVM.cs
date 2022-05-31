using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Products.Changes;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    /*
    * Controls AddProduct Window in Products of ManagerMenu
    */
    class AddProductWindowVM : WindowViewModel<AddProductWindow, AddProductVM>
    {
        public AddProductWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
