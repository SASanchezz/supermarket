using supermarket.Windows.ManagerMenu.Products.Changes;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    internal class EditProductWindowVM : WindowViewModel<EditProductWindow, EditProductVM>
    {
        public EditProductWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
