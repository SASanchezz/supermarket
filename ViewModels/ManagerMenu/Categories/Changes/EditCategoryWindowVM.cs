using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Categories.Changes;

namespace supermarket.ViewModels.ManagerMenu.Categories.Changes
{
    internal class EditCategoryWindowVM : WindowViewModel<EditCategoryWindow, EditCategoryVM>
    {
        public EditCategoryWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}