using supermarket.Windows.ManagerMenu.Employees.Changes;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    /*
     * Controls EditEmployee Window in Employees of ManagerMenu
     */
    internal class EditEmployeeWindowVM : WindowViewModel<EditEmployeeWindow, EditEmployeeVM>
    {
        public EditEmployeeWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
