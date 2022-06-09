using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Categories.Changes;

namespace supermarket.ViewModels.ManagerMenu.Categories.Changes
{
    internal class AddCategoryWindowVM : WindowViewModel<AddCategoryWindow, AddCategoryVM>
    {
        public AddCategoryWindowVM()
        {
            SetResettingSystem();
        }
        
        private void SetResettingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue)
                {
                    return;
                }
            
                // window is shown
                ViewModel.Reset();
            };
        }
    }
}