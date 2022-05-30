using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.ViewModels.ManagerMenu.Employees.Changes;
using supermarket.Windows.ManagerMenu.Employees;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Employees
{
    /* 
     * Controls ManagerEmployees Window
     * Set navigating Windows as ManagerAddEmployee Window, ManagerEditEmployee Window
     */
    internal class EmployeesWindowVM : WindowViewModel<EmployeesWindow, EmployeesVM>
    {
        private WindowVMNavigator<ManagerEmployees> _windowsNavigator;

        private AddEmployeeWindowVM _addEmployeeWindowVM;
        private EditEmployeeWindowVM _editEmployeeWindowVM;

        public EmployeesWindowVM()
        {
            _addEmployeeWindowVM = new AddEmployeeWindowVM();
            _editEmployeeWindowVM = new EditEmployeeWindowVM();

            SetVisibilitySystem(_addEmployeeWindowVM);
            SetVisibilitySystem(_editEmployeeWindowVM);

            _windowsNavigator = new WindowVMNavigator<ManagerEmployees>(new IWindowOpeningVM<ManagerEmployees>[] { ViewModel });
            
            _windowsNavigator.SetWay(ManagerEmployees.AddEmployee, _addEmployeeWindowVM.Window, GoToAddEmployee);
            _windowsNavigator.SetWay(ManagerEmployees.EditEmployee, _editEmployeeWindowVM.Window, GoToEditEmployee);

            Window.Closed += (sender, e) =>
            {
                _addEmployeeWindowVM.Window.Close();
                _editEmployeeWindowVM.Window.Close();
            };
            
            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;
        }

        private void GoToAddEmployee()
        {
            SetDefaultClosedEventHandler(_addEmployeeWindowVM);
        }

        private void GoToEditEmployee()
        {
            if (ViewModel.SelectedEmployee == null)
            {
                throw new Exception("No selected item");
            }

            SetDefaultClosedEventHandler(_editEmployeeWindowVM);

            _editEmployeeWindowVM.ViewModel.SetData(ViewModel.SelectedEmployee);
            ViewModel.SelectedEmployee = null;
        }

        private void SetDefaultClosedEventHandler<WindowType, VMType>(WindowViewModel<WindowType, VMType> windowVM) 
            where WindowType : Window, new()
            where VMType : ViewModel, new()
        {
            string filteredSurname = ViewModel.FilteredSurname;
            string selectedRole = ViewModel.SelectedRole;
            
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue.Equals(false))
                {
                    ViewModel.FilteredSurname = filteredSurname;
                    ViewModel.SelectedRole = selectedRole;
                    ViewModel.UpdateEmployees();
                    //MessageBox.Show("Data updated");
                }
            };
        }

        private void SetVisibilitySystem<WindowType, VMType>(WindowViewModel<WindowType, VMType> windowVM)
            where WindowType : Window, new()
            where VMType : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
    }
}
