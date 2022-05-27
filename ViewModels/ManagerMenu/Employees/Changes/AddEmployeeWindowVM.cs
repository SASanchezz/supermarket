using supermarket.Navigation.WindowVM;
using supermarket.Windows.ManagerMenu.Employees.Changes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    /*
     * Controls ManagerAddEmployee Window
     */
    internal class AddEmployeeWindowVM : WindowViewModel<AddEmployeeWindow, AddEmployeeVM>
    {
        public AddEmployeeWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
