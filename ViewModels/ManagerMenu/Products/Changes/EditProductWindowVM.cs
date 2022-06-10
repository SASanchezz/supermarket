using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Products.Changes;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    internal class EditProductWindowVM : WindowViewModel<EditProductWindow, EditProductVM>
    {
        public EditProductWindowVM()
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
