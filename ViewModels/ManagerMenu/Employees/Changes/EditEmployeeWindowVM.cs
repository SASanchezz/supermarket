using supermarket.Windows.ManagerMenu.Employees.Changes;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    /*
     * Controls ManagerEditEmployee Window
     */
    internal class EditEmployeeWindowVM : WindowViewModel<EditEmployeeWindow, EditEmployeeVM>
    {
        public EditEmployeeWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
