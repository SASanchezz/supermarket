using supermarket.Navigation.WindowVM;
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

            _windowsNavigator = new WindowVMNavigator<ManagerEmployees>(new IWindowOpeningVM<ManagerEmployees>[] { ViewModel });
            
            _windowsNavigator.SetWay(ManagerEmployees.AddEmployee,  GoToAddEmployee);
            _windowsNavigator.SetWay(ManagerEmployees.EditEmployee, GoToEditEmployee);

            Window.Closed += (object sender, EventArgs e) =>
            {
                _addEmployeeWindowVM.Window.Close();
                _editEmployeeWindowVM.Window.Close();
            };
            
            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;

            Window.Show();
        }

        private void GoToAddEmployee()
        {
            SetDefaultClosedEventHandler(_addEmployeeWindowVM);
            _addEmployeeWindowVM.Window.Show();
        }

        private void GoToEditEmployee()
        {
            try
            {
                if (ViewModel.SelectedEmployee == null)
                {
                    throw new Exception("No selected item");
                }

                SetDefaultClosedEventHandler(_addEmployeeWindowVM);

                _editEmployeeWindowVM.ViewModel.SetData(ViewModel.SelectedEmployee);
                ViewModel.SelectedEmployee = null;
                _editEmployeeWindowVM.Window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    
        }

        private void SetDefaultClosedEventHandler<WindowType, VMType>(WindowViewModel<WindowType, VMType> windowVM) 
            where WindowType : Window, new()
            where VMType : ViewModel, new()
        {
            string filteredSurname = ViewModel.FilteredSurname;
            windowVM.Window.Closed += (sender, e) =>
            {
                ViewModel.UpdateEmployees();
                ViewModel.FilteredSurname = filteredSurname;
            };
        }
    }
}
