using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Products.Changes;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    /*
    * Controls AddProduct Window in Products of ManagerMenu
    */
    internal class AddProductWindowVM : WindowViewModel<AddProductWindow, AddProductVM>
    {
        public AddProductWindowVM()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue)
                {
                    return;
                }
            
                // window is shown
                ViewModel.UpdateSelectiveCategories();
            };
        }
    }
}
