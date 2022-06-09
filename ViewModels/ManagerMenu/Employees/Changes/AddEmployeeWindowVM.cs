using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.Employees.Changes;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    /*
     * Controls AddEmployee Window in Employees of ManagerMenu
     */
    internal class AddEmployeeWindowVM : WindowViewModel<AddEmployeeWindow, AddEmployeeVM>
    {
        public AddEmployeeWindowVM()
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
