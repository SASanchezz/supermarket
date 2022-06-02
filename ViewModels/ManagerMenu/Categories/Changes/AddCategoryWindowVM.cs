using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Categories.Changes;

namespace supermarket.ViewModels.ManagerMenu.Categories.Changes
{
    internal class AddCategoryWindowVM : WindowViewModel<AddCategoryWindow, AddCategoryVM>
    {
        public AddCategoryWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}